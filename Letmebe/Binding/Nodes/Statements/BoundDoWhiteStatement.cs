using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundDoWhiteStatement : BoundStatement {
        public readonly BoundStatement Statement;
        public readonly BoundExpression Condition;

        public BoundDoWhiteStatement(BoundStatement statement, BoundExpression condition) {
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
             * @while:
             * <statement>
             * goto @end if not <condition>
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
                Statement.Lowered(),
                gotoEnd,
                gotoWhile,
                endLabel,
            });
        }
    }
}
