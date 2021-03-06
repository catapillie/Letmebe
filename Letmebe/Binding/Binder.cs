using Letmebe.Binding.Nodes;
using Letmebe.Diagnostics;
using Letmebe.Lexing;
using Letmebe.Parsing.Nodes;

namespace Letmebe.Binding {
    internal sealed class Binder {
        public readonly DiagnosticList Diagnostics;

        private Scope scope = new();

        public Binder(DiagnosticList diagnostics) {
            Diagnostics = diagnostics;

            scope.RegisterBuiltInFunction(BuiltInFunctions.PrintFunction);
            scope.RegisterBuiltInFunction(BuiltInFunctions.AskFunction);
        }

        public BoundProgram Bind(ProgramNode program) {
            return new BoundProgram(BindStatementArray(program.Statements));
        }

        private BoundStatement[] BindStatementArray(Statement[] statements)
            => statements.Select(s => BindStatement(s))
                         .ToArray();

        private BoundStatement BindStatement(Statement statement) {
            switch (statement) {
                case BlockStatement s: {
                    ++scope;
                    var boundBlock = new BoundBlockStatement(BindStatementArray(s.Statements));
                    --scope;

                    return boundBlock;
                }

                case IfStatement s: {
                    var boundCondition = BindExpression(s.Condition);
                    var boundStatement = BindStatement(s.ThenStatement);

                    if (Mismatch(boundCondition.Type, BoundPrimitiveType.BooleanPrimitive, inherit: false))
                        Diagnostics.Add(Reports.IfConditionMustBeBoolean());

                    return new BoundIfStatement(boundCondition, boundStatement);
                }

                case UnlessStatement s: {
                    var boundCondition = BindExpression(s.Condition);
                    var boundStatement = BindStatement(s.ThenStatement);

                    if (Mismatch(boundCondition.Type, BoundPrimitiveType.BooleanPrimitive, inherit: false))
                        Diagnostics.Add(Reports.IfConditionMustBeBoolean());

                    return new BoundUnlessStatement(boundCondition, boundStatement);
                }

                case IfOtherwiseStatement s: {
                    var boundCondition = BindExpression(s.Condition);
                    var boundStatement = BindStatement(s.ThenStatement);
                    var boundOtherwiseStatement = BindStatement(s.OtherwiseStatement);

                    if (Mismatch(boundCondition.Type, BoundPrimitiveType.BooleanPrimitive, inherit: false))
                        Diagnostics.Add(Reports.IfOtherwiseConditionMustBeBoolean());

                    return new BoundIfOtherwiseStatement(boundCondition, boundStatement, boundOtherwiseStatement);
                }

                case WhileStatement s: {
                    var boundCondition = BindExpression(s.Condition);
                    var boundStatement = BindStatement(s.Statement);

                    if (Mismatch(boundCondition.Type, BoundPrimitiveType.BooleanPrimitive, inherit: false))
                        Diagnostics.Add(Reports.WhileConditionMustBeBoolean());

                    return new BoundWhileStatement(boundCondition, boundStatement);
                }

                case UntilStatement s: {
                    var boundCondition = BindExpression(s.Condition);
                    var boundStatement = BindStatement(s.Statement);

                    if (Mismatch(boundCondition.Type, BoundPrimitiveType.BooleanPrimitive, inherit: false))
                        Diagnostics.Add(Reports.UntilConditionMustBeBoolean());

                    return new BoundUntilStatement(boundCondition, boundStatement);
                }

                case DoStatement s: {
                    return new BoundDoStatement(BindStatement(s.Statement));
                }

                case DoWhileStatement s: {
                    var boundStatement = BindStatement(s.Statement);
                    var boundCondition = BindExpression(s.Condition);

                    if (Mismatch(boundCondition.Type, BoundPrimitiveType.BooleanPrimitive, inherit: false))
                        Diagnostics.Add(Reports.DoWhileConditionMustBeBoolean());

                    return new BoundDoWhileStatement(boundStatement, boundCondition);
                }

                case DoUntilStatement s: {
                    var boundStatement = BindStatement(s.Statement);
                    var boundCondition = BindExpression(s.Condition);

                    if (Mismatch(boundCondition.Type, BoundPrimitiveType.BooleanPrimitive, inherit: false))
                        Diagnostics.Add(Reports.DoUntilConditionMustBeBoolean());

                    return new BoundDoUntilStatement(boundStatement, boundCondition);
                }

                case ForeverStatement s: {
                    return new BoundForeverStatement(BindStatement(s.Statement));
                }

                case RepeatTimesStatement s: {
                    var boundAmount = BindExpression(s.Amount);
                    var boundStatement = BindStatement(s.Statement);

                    if (Mismatch(boundAmount.Type, BoundPrimitiveType.IntegerPrimitive, inherit: false))
                        Diagnostics.Add(Reports.RepeatTimesAmountMustBeInteger());

                    return new BoundRepeatTimesStatement(boundAmount, boundStatement);
                }

                case ExpressionStatement s: {
                    if (s.IsInvalid)
                        Diagnostics.Add(Reports.ExpressionStatementMustBeFunctionCallOrAssignment());

                    var boundExpr = BindExpression(s.Expression);
                    return new BoundExpressionStatement(boundExpr);
                }

                case VariableDeclarationStatement s: {
                    var name = s.Identifier.Str;
                    var type = BindTypeExpression(s.TypeExpression);
                    var value = BindExpression(s.Expression);

                    if (!scope.TryRegisterVariable(type, name, out var symbol))
                        Diagnostics.Add(Reports.VariableAlreadyExists(symbol));

                    if (Mismatch(value.Type, symbol.Type))
                        Diagnostics.Add(Reports.CannotAssignTypeToTargetType(value.Type, symbol.Type));

                    return new BoundVariableDefinitionStatement(symbol, value);
                }

                case FunctionDefinitionStatement s: {
                    return BindFunctionDefinition(s.Sentence, BindTypeExpression(s.TypeExpression), s.Body);
                }

                case VoidFunctionDefinitionStatement s: {
                    return BindFunctionDefinition(s.Sentence, BoundPrimitiveType.VoidPrimitive, s.Body);
                }

                case ReturnStatement s: {
                    if (scope.ReturnType is null)
                        Diagnostics.Add(Reports.ReturnStatementMustBeUsedWithinFunction());
                            
                    if (s.Expression is not null) {
                        var boundExpression = BindExpression(s.Expression);

                        if (scope.ReturnType != null && Mismatch(boundExpression.Type, scope.ReturnType))
                            Diagnostics.Add(Reports.ExpectedReturnTypeInsteadOf(scope.ReturnType, boundExpression.Type));
                        return new BoundReturnStatement(boundExpression, scope.ReturningFunction!);
                    }

                    return new BoundVoidReturnStatement(scope.ReturningFunction!);
                }

                case EmptyStatement:
                    return new BoundEmptyStatement();
            }
            
            throw new Exception($"Unimplemented statement binding for type {statement.GetType()}");
        }

        private BoundFunctionDefinitionStatement BindFunctionDefinition(DefinitionSentence sentence, BoundType returnType, Statement body) {
            BoundStatement boundBody;
            List<(BoundType Type, string Name)> declaredParameters = new();

            BoundFunctionTemplate template;

            if (sentence.Words.Length > 0) {
                List<BoundFunctionWord> words = new();

                bool hasIdentifiers = false;
                foreach (var w in sentence.Words) {

                    if (w is DefinitionParameterListWord paramList) {
                        foreach (var param in paramList.Parameters) {
                            var boundType = BindTypeExpression(param.TypeExpression);
                            string name = param.IdentifierToken.Str;

                            declaredParameters.Add((boundType, name));
                            words.Add(new BoundFunctionParameterWord(boundType));
                        }
                    } else {
                        hasIdentifiers = true;
                        string identifier = ((DefinitionIdentifierWord)w).IdentifierToken.Str;
                        words.Add(new BoundFunctionIdentifierWord(identifier));
                    }

                }

                template = new BoundFunctionTemplate(words.ToArray());

                if (!hasIdentifiers)
                    Diagnostics.Add(Reports.FunctionSignatureMustHaveIdentifier());
            } else
                template = new BoundFunctionTemplate(Array.Empty<BoundFunctionWord>());

            if (!scope.TryRegisterFunction(returnType, template, out var functionSymbol))
                Diagnostics.Add(Reports.FunctionAlreadyDefined(functionSymbol));

            ++scope; // This is placed before the arguments are declared, and after the function is registerd.
            scope.ReturnType = returnType;
            scope.ReturningFunction = functionSymbol;

            // Store the parameter symbols in the function symbol AFTER they are declared.
            functionSymbol.ParameterSymbols = declaredParameters.Select(param => {
                (BoundType type, string name) = param;
                if (!scope.TryRegisterVariable(type, name, out var symbol))
                    Diagnostics.Add(Reports.FunctionParameterAlreadyDeclared(symbol));
                return symbol;
            }).ToArray();

            // BindStatement(BlockStatement) would advance the scope forward, making shadowing the function's parameters possible.
            // We don't want that, so let's bind it manually.
            if (body is BlockStatement block)
                boundBody = new BoundBlockStatement(BindStatementArray(block.Statements));
            else
                boundBody = BindStatement(body);

            --scope;

            return new BoundFunctionDefinitionStatement(functionSymbol, boundBody);
        }

        private BoundType BindTypeExpression(TypeExpression typeExpr) {
            switch (typeExpr) {
                case PrimaryTypeExpression t:  {
                    if (t.TypeToken.Kind == TokenKind.IDENTIFIER)
                        return GetBoundUserType(t.TypeToken);
                    if (t.TypeToken.Kind == TokenKind.INT)
                        return BoundPrimitiveType.IntegerPrimitive;
                    if (t.TypeToken.Kind == TokenKind.FLOAT)
                        return BoundPrimitiveType.FloatPrimitive;
                    if (t.TypeToken.Kind == TokenKind.BOOL)
                        return BoundPrimitiveType.BooleanPrimitive;
                    if (t.TypeToken.Kind == TokenKind.STR)
                        return BoundPrimitiveType.StringPrimitive;
                    if (t.TypeToken.Kind == TokenKind.CHAR)
                        return BoundPrimitiveType.CharacterPrimitive;
                    if (t.TypeToken.Kind == TokenKind.OBJ)
                        return BoundPrimitiveType.ObjectPrimitive;
                    if (t.TypeToken.Kind == TokenKind.TYPE)
                        return BoundPrimitiveType.TypePrimitive;
                    break;
                }

                case ParenthesizedTypeExpression t: {
                    return BindTypeExpression(t.TypeExpression);
                }

                case ArrayTypeExpression t: {
                    var boundTypeExpr = BindTypeExpression(t.TypeExpression);
                    return new BoundArrayType(boundTypeExpr);
                }

                case TupleTypeExpression t: {
                    var boundTypeExprs = t.TypeExpressions.Select(t => BindTypeExpression(t)).ToArray();
                    return new BoundTupleType(boundTypeExprs);
                }

                case FunctionTypeExpression t: {
                    var boundParameterTypeExpr = BindTypeExpression(t.ParameterTypeExpression);
                    var boundReturnTypeExpr = BindTypeExpression(t.ReturnTypeExpression);
                    return new BoundFunctionType(boundParameterTypeExpr, boundReturnTypeExpr);
                }

                case GenericTypeExpression t: {
                    var boundUserType = GetBoundUserType(t.Identifier, t.TypeExpressions.Length);
                    var boundTypeExprs = t.TypeExpressions.Select(t => BindTypeExpression(t)).ToArray();
                    return new BoundGenericType(boundUserType, boundTypeExprs);
                }
            }

            return BoundUnknownType.Unknown;
        }

        private BoundExpression BindExpression(Expression expr) {
            switch (expr) {
                case IntegerLiteral e: {
                    int value = (int)e.IntegerToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.IntegerPrimitive, value);
                }

                case DecimalLiteral e: {
                    float value = (float)e.FloatToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.FloatPrimitive, value);
                }

                case BooleanLiteral e: {
                    bool value = (bool)e.BooleanToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.BooleanPrimitive, value);
                }

                case StringLiteral e: {
                    string value = (string)e.StringToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.StringPrimitive, value);
                }

                case CharacterLiteral e: {
                    char value = (char)e.CharacterToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.CharacterPrimitive, value);
                }

                case VariableLiteral e: {
                    return GetBoundVariable(e.IdentifierToken);
                }

                case AssignmentExpression e: {
                    var boundTarget = BindExpression(e.Target);
                    var boundValue = BindExpression(e.Value);

                    if (Mismatch(boundValue.Type, boundTarget.Type))
                        Diagnostics.Add(Reports.CannotAssignTypeToTargetType(boundValue.Type, boundTarget.Type));

                    return new BoundAssignmentExpression(boundTarget, boundValue);
                }

                case ParenthesizedExpression e: {
                    return BindExpression(e.Expression);
                }

                case BinaryOperationExpression e: {
                    var boundLeft = BindExpression(e.LeftOperand);
                    var opToken = e.Op.Kind;
                    var boundRight = BindExpression(e.RightOperand);

                    foreach (BoundBinaryOperator op in BoundBinaryOperator.BinaryOperators) {
                        if (op.OperatorToken == opToken && boundLeft.Type.Is(op.LeftType, inherit: false) &&
                                                           boundRight.Type.Is(op.RightType, inherit: false))
                            return new BoundBinaryOperation(boundLeft, op, boundRight);
                    }

                    if (boundLeft.Type.IsKnown && boundRight.Type.IsKnown)
                        Diagnostics.Add(Reports.UndefinedBinaryOperator(opToken, boundLeft.Type, boundRight.Type));

                    BoundBinaryOperator unknownOperator = new(opToken, BoundBinaryOperator.Operation.Unknown, boundLeft.Type, boundRight.Type, BoundUnknownType.Unknown);
                    return new BoundBinaryOperation(boundLeft, unknownOperator, boundRight);
                }

                case UnaryOperationExpression e: {
                    var boundOperand = BindExpression(e.Operand);
                    var opToken = e.Op.Kind;

                    foreach (BoundUnaryOperator op in BoundUnaryOperator.UnaryOperators) {
                        if (op.OperatorToken == opToken && boundOperand.Type.Is(op.OperandType, inherit: false))
                            return new BoundUnaryOperation(boundOperand, op);
                    }

                    if (boundOperand.Type.IsKnown)
                        Diagnostics.Add(Reports.UndefinedUnaryOperator(opToken, boundOperand.Type));

                    BoundUnaryOperator unknownOperator = new(opToken, BoundUnaryOperator.Operation.Unknown, boundOperand.Type, BoundUnknownType.Unknown);
                    return new BoundUnaryOperation(boundOperand, unknownOperator);
                }

                case MemberLookupExpression: {
                    throw new NotImplementedException("TODO: Member lookup");
                }

                case ArrayExpression e: {
                    var boundValues = e.Values.Select(v => BindExpression(v)).ToArray();

                    if (boundValues.Length > 0) {
                        var type = FindLeastInheritedCommonType(boundValues.Select(v => v.Type).ToArray());

                        return new BoundArrayExpression(boundValues, type);
                    } else
                        return new BoundArrayExpression(Array.Empty<BoundExpression>(), BoundUnknownType.Matching);
                }

                case IndexingExpression e: {
                    var boundExpression = BindExpression(e.Expression);
                    var boundIndexExpression = BindExpression(e.IndexExpression);

                    if (boundExpression.Type is BoundArrayType array) {
                        if (Mismatch(boundIndexExpression.Type, BoundPrimitiveType.IntegerPrimitive, inherit: false))
                            Diagnostics.Add(Reports.ArrayMustBeIndexedWithInteger());

                        BoundIndexerOperator indexingOpertator = new(boundExpression.Type, BoundIndexerOperator.Operation.ArrayIndexing, new[] { boundIndexExpression.Type }, array.Type);
                        return new BoundIndexingExpression(boundExpression, boundIndexExpression, indexingOpertator);
                    }

                    // TODO: multiple arguments indexation
                    foreach (BoundIndexerOperator op in BoundIndexerOperator.IndexerOperators) {
                        if (boundExpression.Type.Is(op.IndexedType, inherit: false) && boundIndexExpression.Type.Is(op.IndexerTypes[0], inherit: false))
                            return new BoundIndexingExpression(boundExpression, boundIndexExpression, op);
                    }

                    if (boundExpression.Type.IsKnown && boundIndexExpression.Type.IsKnown)
                        Diagnostics.Add(Reports.UndefinedIndexerOperator(boundExpression.Type, boundIndexExpression.Type));

                    BoundIndexerOperator unknownOperator = new(boundExpression.Type, BoundIndexerOperator.Operation.Unknown, new[] { boundIndexExpression.Type }, BoundUnknownType.Unknown);
                    return new BoundIndexingExpression(boundExpression, boundIndexExpression, unknownOperator);
                }

                case FunctionCallExpression e: {
                    List<BoundExpression> parameters = new();
                    bool allTypesKnown = true;

                    var boundWords = e.Words.Select<FunctionCallWord, BoundFunctionWord>(word => {
                        if (word is FunctionCallIdentifier identifierWord)
                            return new BoundFunctionIdentifierWord(identifierWord.Variable.IdentifierToken.Str);
                        else {
                            var boundParameter = BindExpression(((FunctionCallParameter)word).ParameterExpression);
                            allTypesKnown &= boundParameter.Type.IsKnown;
                            parameters.Add(boundParameter);
                            return new BoundFunctionParameterWord(boundParameter.Type);
                        }
                    }).ToArray();

                    var template = new BoundFunctionTemplate(boundWords);

                    if (allTypesKnown & !scope.TryLookupFunction(template, out var function))
                        Diagnostics.Add(Reports.UndefinedFunction(template));

                    return new BoundFunctionCall(function, parameters.ToArray());
                }
            }

            return new BoundUnknownExpression();
        }

        private static bool Mismatch(BoundType type, BoundType target, bool inherit = true)
            => type.IsKnown && target.IsKnown && !type.Is(target, inherit);

        private static BoundType FindLeastInheritedCommonType(BoundType[] types) {
            if (types.Length == 0)
                return BoundUnknownType.Matching;
            else if (types.Length == 1)
                return types[0];

            List<BoundType> condensed = new();
            foreach (var type in types) {
                if (!condensed.Any(t => type.Is(t, inherit: false)))
                    condensed.Add(type);
            }

            if (condensed.Count == 1)
                return condensed[0];

            condensed.Sort((a, b) => a.Is(b) ? -1 : 1);
            var firstCommon = condensed.FirstOrDefault(type => condensed.All(t => t.Is(type)));
            if (firstCommon != null)
                return firstCommon;

            var last = condensed.Last();
            do {
                last = last.Base;
            } while (last != null && !condensed.All(t => t.Is(last)));

            return BoundPrimitiveType.ObjectPrimitive;
        }

        private BoundUserType GetBoundUserType(Token id, int genericArgumentCount = 0) {
            var name = id.Str;
            if (!scope.TryLookupType(name, genericArgumentCount, out var type)) {
                Diagnostics.Add(Reports.UndefinedTypeInCurrentScope(name, genericArgumentCount));
            }
            return type;
        }

        private BoundSymbol GetBoundVariable(Token id) {
            var name = id.Str;
            if (!scope.TryLookupVariable(name, out var variable)) {
                Diagnostics.Add(Reports.UndefinedVariableInScope(name));
            }
            return variable;
        }
    }
}
