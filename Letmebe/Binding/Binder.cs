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
        }

        public BoundProgram Bind(ProgramNode program) {
            return new BoundProgram(BindStatementArray(program.Statements));
        }

        private BoundStatement[] BindStatementArray(Statement[] statements)
            => statements.Select(s => BindStatement(s))
                         .Where(s => s is not null)
                         .ToArray();

        private BoundStatement BindStatement(Statement statement) {
            switch (statement) {
                case BlockStatement blockStatement: {
                    ++scope;
                    var boundBlock = new BoundBlockStatement(BindStatementArray(blockStatement.Statements));
                    --scope;

                    return boundBlock;
                }

                case DoStatement doStatement: {
                    return new BoundDoStatement(BindStatement(doStatement.Statement));
                }

                case DoWhileStatement doWhile: {
                    var boundStatement = BindStatement(doWhile.Statement);
                    var boundCondition = BindExpression(doWhile.Condition);

                    if (boundCondition.Type is not null && boundCondition.Type != BoundPrimitiveType.BooleanPrimitive)
                        Diagnostics.Add(Reports.DoWhileConditionMustBeBoolean());

                    return new BoundDoWhiteStatement(boundStatement, boundCondition);
                }

                case DoUntilStatement doUntil: {
                    var boundStatement = BindStatement(doUntil.Statement);
                    var boundCondition = BindExpression(doUntil.Condition);

                    if (boundCondition.Type is not null && boundCondition.Type != BoundPrimitiveType.BooleanPrimitive)
                        Diagnostics.Add(Reports.DoUntilConditionMustBeBoolean());

                    return new BoundDoUntilStatement(boundStatement, boundCondition);
                }

                case ExpressionStatament expressionStatament: {
                    if (expressionStatament.IsInvalid)
                        Diagnostics.Add(Reports.ExpressionStatementMustBeFunctionCall());

                    var boundExpr = BindExpression(expressionStatament.Expression);
                    return new BoundExpressionStatement(boundExpr);
                }

                case ForeverStatement foreverStatement: {
                    return new BoundForeverStatement(BindStatement(foreverStatement.Statement));
                }

                case RepeatTimesStatement repeatTimesStatement: {
                    var boundAmount = BindExpression(repeatTimesStatement.Amount);
                    var boundStatement = BindStatement(repeatTimesStatement.Statement);

                    if (boundAmount.Type is not null && boundAmount.Type != BoundPrimitiveType.IntegerPrimitive)
                        Diagnostics.Add(Reports.RepeatTimesAmountMustBeInteger());

                    return new BoundRepeatTimesStatement(boundAmount, boundStatement);
                }

                case VariableDeclarationStatement variableDeclarationStatement: {
                    var name = variableDeclarationStatement.Identifier.Str;
                    var type = BindTypeExpression(variableDeclarationStatement.TypeExpression);
                    var value = BindExpression(variableDeclarationStatement.Expression);

                    if (!scope.TryRegisterVariable(type, name, out var symbol))
                        Diagnostics.Add(Reports.VariableAlreadyExists(symbol));

                    return new BoundVariableDefinitionStatement(symbol, value);
                }

                case EmptyStatement:
                    return null!;
            }

            throw new Exception($"Unimplemented statement binding for type {statement.GetType()}");
        }

        private BoundType BindTypeExpression(TypeExpression typeExpr) {
            switch (typeExpr) {
                case PrimaryTypeExpression primaryType:  {
                    if (primaryType.TypeToken.Kind == TokenKind.IDENTIFIER)
                        return GetBoundUserType(primaryType.TypeToken);
                    if (primaryType.TypeToken.Kind == TokenKind.INT)
                        return BoundPrimitiveType.IntegerPrimitive;
                    if (primaryType.TypeToken.Kind == TokenKind.FLOAT)
                        return BoundPrimitiveType.FloatPrimitive;
                    if (primaryType.TypeToken.Kind == TokenKind.BOOL)
                        return BoundPrimitiveType.BooleanPrimitive;
                    if (primaryType.TypeToken.Kind == TokenKind.STR)
                        return BoundPrimitiveType.StringPrimitive;
                    if (primaryType.TypeToken.Kind == TokenKind.CHAR)
                        return BoundPrimitiveType.CharacterPrimitive;
                    if (primaryType.TypeToken.Kind == TokenKind.OBJ)
                        return BoundPrimitiveType.ObjectPrimitive;
                    if (primaryType.TypeToken.Kind == TokenKind.TYPE)
                        return BoundPrimitiveType.TypePrimitive;
                    break;
                }

                case ParenthesizedTypeExpression parenthesizedTypeExpression: {
                    return BindTypeExpression(parenthesizedTypeExpression.TypeExpression);
                }

                case ArrayTypeExpression arrayTypeExpression: {
                    var boundTypeExpr = BindTypeExpression(arrayTypeExpression.TypeExpression);
                    return new BoundArrayType(boundTypeExpr);
                }

                case TupleTypeExpression tupleTypeExpression: {
                    var boundTypeExprs = tupleTypeExpression.TypeExpressions.Select(t => BindTypeExpression(t)).ToArray();
                    return new BoundTupleType(boundTypeExprs);
                }

                case FunctionTypeExpression functionTypeExpression: {
                    var boundParameterTypeExpr = BindTypeExpression(functionTypeExpression.ParameterTypeExpression);
                    var boundReturnTypeExpr = BindTypeExpression(functionTypeExpression.ReturnTypeExpression);
                    return new BoundFunctionType(boundParameterTypeExpr, boundReturnTypeExpr);
                }

                case GenericTypeExpression genericTypeExpression: {
                    var boundUserType = GetBoundUserType(genericTypeExpression.Identifier, genericTypeExpression.TypeExpressions.Length);
                    var boundTypeExprs = genericTypeExpression.TypeExpressions.Select(t => BindTypeExpression(t)).ToArray();
                    return new BoundGenericType(boundUserType, boundTypeExprs);
                }
            }

            throw new Exception($"Unimplemented type expression binding for type {typeExpr.GetType()}");
        }

        private BoundExpression BindExpression(Expression expr) {
            switch (expr) {
                case IntegerLiteral integerLiteral: {
                    int value = (int)integerLiteral.IntegerToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.IntegerPrimitive, value);
                }

                case DecimalLiteral floatLiteral: {
                    float value = (float)floatLiteral.FloatToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.FloatPrimitive, value);
                }

                case BooleanLiteral booleanLiteral: {
                    bool value = (bool)booleanLiteral.BooleanToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.BooleanPrimitive, value);
                }

                case StringLiteral stringLiteral: {
                    string value = (string)stringLiteral.StringToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.StringPrimitive, value);
                }

                case CharacterLiteral characterLiteral: {
                    char value = (char)characterLiteral.CharacterToken.Value;
                    return new BoundLiteral(BoundPrimitiveType.CharacterPrimitive, value);
                }

                case VariableLiteral variableLiteral: {
                    return GetBoundVariable(variableLiteral.IdentifierToken);
                }

                case ParenthesizedExpression parenthesizedExpression: {
                    return BindExpression(parenthesizedExpression.Expression);
                }

                case BinaryOperationExpression binaryOperationExpression: {
                    var boundLeft = BindExpression(binaryOperationExpression.LeftOperand);
                    var opToken = binaryOperationExpression.Op.Kind;
                    var boundRight = BindExpression(binaryOperationExpression.RightOperand);

                    foreach (BoundBinaryOperator op in BoundBinaryOperator.BinaryOperators) {
                        if (op.OperatorToken == opToken && op.LeftType == boundLeft.Type && op.RightType == boundRight.Type)
                            return new BoundBinaryOperation(boundLeft, op, boundRight);
                    }

                    if (boundLeft.Type is not null && boundRight.Type is not null)
                        Diagnostics.Add(Reports.UndefinedBinaryOperator(opToken, boundLeft.Type, boundRight.Type));

                    BoundBinaryOperator unknownOperator = new(opToken, boundLeft.Type!, boundRight.Type!, null!);
                    return new BoundBinaryOperation(boundLeft, unknownOperator, boundRight);
                }

                case UnaryOperationExpression unaryOperationExpression: {
                    var boundOperand = BindExpression(unaryOperationExpression.Operand);
                    var opToken = unaryOperationExpression.Op.Kind;

                    foreach (BoundUnaryOperator op in BoundUnaryOperator.UnaryOperators) {
                        if (op.OperatorToken == opToken && op.OperandType == boundOperand.Type)
                            return new BoundUnaryOperation(boundOperand, op);
                    }

                    if (boundOperand.Type is not null)
                        Diagnostics.Add(Reports.UndefinedUnaryOperator(opToken, boundOperand.Type));

                    BoundUnaryOperator unknownOperator = new(opToken, boundOperand.Type!, null!);
                    return new BoundUnaryOperation(boundOperand, unknownOperator);
                }

                case MemberLookupExpression: {
                    throw new NotImplementedException("TODO: Member lookup");
                }

                case IndexingExpression indexingExpression: {
                    var boundExpression = BindExpression(indexingExpression.Expression);
                    var boundIndexExpression = BindExpression(indexingExpression.IndexExpression);

                    foreach (BoundIndexerOperator op in BoundIndexerOperator.IndexerOperators) {
                        if (op.IndexedType == boundExpression.Type && op.IndexerTypes[0] == boundIndexExpression.Type)
                            return new BoundIndexingExpression(boundExpression, boundIndexExpression, op);
                    }

                    if (boundExpression.Type is not null && boundIndexExpression.Type is not null)
                        Diagnostics.Add(Reports.UndefinedIndexerOperator(boundExpression.Type, boundIndexExpression.Type));

                    BoundIndexerOperator unknownOperator = new(boundExpression.Type!, new[] { boundIndexExpression.Type! }, null!);
                    return new BoundIndexingExpression(boundExpression, boundIndexExpression, unknownOperator);
                }

                case FunctionCallExpression functionCallExpression: {
                    List<BoundExpression> parameters = new();
                    var boundWords = functionCallExpression.Words.Select<FunctionCallWord, BoundFunctionWord>(word => {
                        if (word is FunctionCallIdentifier identifierWord)
                            return new BoundFunctionIdentifierWord(identifierWord.Variable.IdentifierToken.Str);
                        else {
                            var boundParameter = BindExpression(((FunctionCallParameter)word).ParameterExpression);
                            return new BoundFunctionParameterWord(boundParameter.Type);
                        }
                    }).ToArray();

                    var template = new BoundFunctionTemplate(boundWords);

                    if (!scope.TryLookupFunction(template, out var function))
                        Diagnostics.Add(Reports.UndefinedFunction(template));

                    return new BoundFunctionCall(function, parameters.ToArray());
                }
            }

            throw new Exception($"Unimplemented expression binding for type {expr.GetType()}");
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
