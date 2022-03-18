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
    }
}
