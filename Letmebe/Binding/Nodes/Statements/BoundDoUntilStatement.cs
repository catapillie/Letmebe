using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundDoUntilStatement : BoundStatement {
        public readonly BoundStatement Statement;
        public readonly BoundExpression Condition;

        public BoundDoUntilStatement(BoundStatement statement, BoundExpression condition) {
            Statement = statement;
            Condition = condition;
        }

        public override IEnumerable Children() {
            yield return Statement;
            yield return Condition;
        }
    }
}
