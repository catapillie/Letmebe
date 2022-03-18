using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundProgram : BoundNode {
        public readonly BoundStatement[] Statements;

        public BoundProgram(BoundStatement[] statements) {
            Statements = statements;
        }

        public override IEnumerable Children() {
            foreach (var statement in Statements)
                yield return statement;
        }
    }
}
