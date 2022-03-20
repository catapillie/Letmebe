using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundDoUntilStatement : BoundStatement {
        public readonly BoundStatement Statement;
        public readonly BoundExpression Condition;

        public BoundDoUntilStatement(BoundStatement statement, BoundExpression condition) {
            Statement = statement;
            Condition = condition;
        }

        public override IEnumerable Children() {
            yield return Statement;
            yield return Condition;
        }

        public override BoundStatement Lowered() {
            /*
             * ...
             * @until:
             * <statement>
             * goto @end if <condition>
             * goto @while
             * @end
             * ...
             */

            var untilLabel = new BoundLabelStatement("@while");
            var endLabel = new BoundLabelStatement("@end");

            var gotoEnd = new BoundConditionalGotoStatement(endLabel, Condition, negated: false);
            var gotoUntil = new BoundGotoStatement(untilLabel);

            return new BoundBlockStatement(new[] {
                untilLabel,
                Statement.Lowered(),
                gotoEnd,
                gotoUntil,
                endLabel,
            });
        }
    }
}
