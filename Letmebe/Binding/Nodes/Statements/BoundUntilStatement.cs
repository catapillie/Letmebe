using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundUntilStatement : BoundStatement {
        public readonly BoundExpression Condition;
        public readonly BoundStatement Statement;

        public BoundUntilStatement(BoundExpression condition, BoundStatement statement) {
            Condition = condition;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return Condition;
            yield return Statement;
        }
    }
}
