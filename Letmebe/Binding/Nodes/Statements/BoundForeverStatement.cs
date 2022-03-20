using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundForeverStatement : BoundStatement {
        public readonly BoundStatement Statement;

        public BoundForeverStatement(BoundStatement statement) {
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return Statement;
        }

        public override BoundStatement Lowered() {
            /*
             * ...
             * @forever
             * <statement>
             * goto forever
             * ...
             */

            var foreverLabel = new BoundLabelStatement("@forever");
            var gotoLabel = new BoundGotoStatement(foreverLabel);

            return new BoundBlockStatement(new[] {
                foreverLabel,
                Statement.Lowered(),
                gotoLabel,
            });
        }
    }
}
