using System.Collections;

namespace Letmebe.Binding.Nodes.Statements {
    internal sealed class BoundUnlessStatement : BoundStatement {
        public readonly BoundExpression Condition;
        public readonly BoundStatement Statement;

        public BoundUnlessStatement(BoundExpression condition, BoundStatement statement) {
            Condition = condition;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return Condition;
            yield return Statement;
        }
    }
}
