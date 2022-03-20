using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundIfOtherwiseStatement : BoundStatement {
        public readonly BoundExpression Condition;
        public readonly BoundStatement Statement, OtherwiseStatement;

        public BoundIfOtherwiseStatement(BoundExpression condition, BoundStatement statement, BoundStatement otherwiseStatement) {
            Condition = condition;
            Statement = statement;
            OtherwiseStatement = otherwiseStatement;
        }

        public override IEnumerable Children() {
            yield return Condition;
            yield return Statement;
            yield return OtherwiseStatement;
        }

        public override BoundStatement Lowered() {
            /*
             * ...
             * goto @other if not <condition>
             * <statement>
             * goto @end
             * @other
             * <other>
             * @end
             * ...
             */

            var otherLabel = new BoundLabelStatement("@other");
            var endLabel = new BoundLabelStatement("@end");
            var gotoOther = new BoundConditionalGotoStatement(otherLabel, Condition, negated: true);
            var gotoEnd = new BoundGotoStatement(endLabel);

            return new BoundBlockStatement(new[] {
                gotoOther,
                Statement.Lowered(),
                gotoEnd,
                otherLabel,
                OtherwiseStatement.Lowered(),
                endLabel,
            });
        }
    }
}
