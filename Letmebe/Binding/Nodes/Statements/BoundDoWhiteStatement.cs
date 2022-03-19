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
    }
}
