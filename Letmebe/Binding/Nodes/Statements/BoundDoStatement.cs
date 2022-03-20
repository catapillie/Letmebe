using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundDoStatement : BoundStatement {
        public readonly BoundStatement Statement;

        public BoundDoStatement(BoundStatement statement) {
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return Statement;
        }

        public override BoundStatement Lowered()
            => new BoundDoStatement(Statement.Lowered());
    }
}
