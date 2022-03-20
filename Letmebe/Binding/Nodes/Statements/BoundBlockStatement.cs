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

        public override BoundStatement Lowered() {
            BoundStatement[] loweredBody = new BoundStatement[Body.Length];
            for (int i = 0; i < Body.Length; i++)
                loweredBody[i] = Body[i].Lowered();
            return new BoundBlockStatement(loweredBody);
        }
    }
}
