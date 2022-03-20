using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundRepeatTimesStatement : BoundStatement {
        public readonly BoundExpression Amount;
        public readonly BoundStatement Statement;

        public BoundRepeatTimesStatement(BoundExpression amount, BoundStatement statement) {
            Amount = amount;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return Amount;
            yield return Statement;
        }

        public override BoundStatement Lowered() {
            /*
             * ...
             * let amount be int <value>;
             * @repeat
             * <statement>
             * amount <- amount - 1;
             * goto @repeat if amount > 0
             * ...
             */

            var amountSymbol = new BoundSymbol(BoundPrimitiveType.IntegerPrimitive, "amount");

            var amountDeclaration = new BoundVariableDefinitionStatement(amountSymbol, Amount);
            var repeatLabel = new BoundLabelStatement("@repeat");

            // (amount <- (amount - (1)))
            var amountDecrementStatement = new BoundExpressionStatement(
                new BoundAssignmentExpression(
                    amountSymbol,
                    new BoundBinaryOperation(
                        amountSymbol,
                        new(Lexing.TokenKind.MINUS, BoundBinaryOperator.Operation.IntegerSubtraction, BoundPrimitiveType.IntegerPrimitive),
                        new BoundLiteral(BoundPrimitiveType.IntegerPrimitive, 1)
                    )
                )
            );

            // (amount > (0))
            var amountCheckExpression = new BoundBinaryOperation(
                amountSymbol,
                new(Lexing.TokenKind.RIGHTCHEVRON, BoundBinaryOperator.Operation.IntegerGreaterThan, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
                new BoundLiteral(BoundPrimitiveType.IntegerPrimitive, 0)
            );

            var gotoRepeat = new BoundConditionalGotoStatement(repeatLabel, amountCheckExpression, negated: false);

            return new BoundBlockStatement(new[] {
                amountDeclaration,
                repeatLabel,
                Statement.Lowered(),
                amountDecrementStatement,
                gotoRepeat,
            });
        }
    }
}
