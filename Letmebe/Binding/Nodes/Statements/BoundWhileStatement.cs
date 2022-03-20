using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundWhileStatement : BoundStatement {
        public readonly BoundExpression Condition;
        public readonly BoundStatement Statement;

        public BoundWhileStatement(BoundExpression condition, BoundStatement statement) {
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
             * @while:
             * goto @end if not <condition>
             * <statement>
             * goto @while
             * @end
             * ...
             */

            var whileLabel = new BoundLabelStatement("@while");
            var endLabel = new BoundLabelStatement("@end");

            var gotoEnd = new BoundConditionalGotoStatement(endLabel, Condition, negated: true);
            var gotoWhile = new BoundGotoStatement(whileLabel);

            return new BoundBlockStatement(new[] {
                whileLabel,
                gotoEnd,
                Statement.Lowered(),
                gotoWhile,
                endLabel,
            });
        }
    }
}
