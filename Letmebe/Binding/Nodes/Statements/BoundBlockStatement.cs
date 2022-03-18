using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundBlockStatement : BoundStatement {
        public readonly BoundStatement[] Body;

        public BoundBlockStatement(BoundStatement[] body) {
            Body = body;
        }

        public override IEnumerable Children() {
            foreach (var statement in Body)
                yield return statement;
        }
    }
}
