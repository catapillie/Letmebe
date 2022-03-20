using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundUnlessStatement : BoundStatement {
        public readonly BoundExpression Condition;
        public readonly BoundStatement Statement;

        public BoundUnlessStatement(BoundExpression condition, BoundStatement statement) {
            Condition = condition;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return Condition;
            yield return Statement;
        }

        public override BoundStatement Lowered() {
            /*
             * ...
             * goto @end if <condition>
             * <statement>
             * @end
             * ...
             */

            var endLabel = new BoundLabelStatement("@end");
            var gotoEnd = new BoundConditionalGotoStatement(endLabel, Condition, negated: false);

            return new BoundBlockStatement(new[] {
                gotoEnd,
                Statement.Lowered(),
                endLabel,
            });
        }
    }
}
