using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundUntilStatement : BoundStatement {
        public readonly BoundExpression Condition;
        public readonly BoundStatement Statement;

        public BoundUntilStatement(BoundExpression condition, BoundStatement statement) {
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
             * @until:
             * goto @end if <condition>
             * <statement>
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
                gotoEnd,
                Statement.Lowered(),
                gotoUntil,
                endLabel,
            });
        }
    }
}
