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

        public BoundProgram Rewritten() {
            BoundStatement[] loweredStatements = new BoundStatement[Statements.Length];
            for (int i = 0; i < Statements.Length; i++)
                loweredStatements[i] = Statements[i].Lowered();
            return new BoundProgram(loweredStatements);
        }
    }
}
