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

        public BoundNode Bind(SyntaxNode root) {
            return root switch {
                Expression expression => BindExpression(expression),
                Statement statement => BindStatement(statement),
                _ => null!,
            };
        }

        private BoundStatement BindStatement(Statement statement) {

            throw new Exception("Given Statement was not a type expression");
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

            throw new Exception($"Unimplemented type_expression binding for type {typeExpr.GetType()}");
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
