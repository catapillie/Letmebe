using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundRepeatTimesStatement : BoundStatement {
        public readonly BoundExpression Amount;
        public readonly BoundStatement Statement;

        public BoundRepeatTimesStatement(BoundExpression amount, BoundStatement statement) {
            Amount = amount;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return Amount;
            yield return Statement;
        }

        // TODO: Lowering (needs assignment expression)
    }
}
