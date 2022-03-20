using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundAssignmentExpression : BoundExpression {
        public readonly BoundExpression Target;
        public readonly BoundExpression Value;

        public override BoundType Type => Target.Type;

        public BoundAssignmentExpression(BoundExpression target, BoundExpression value) {
            Target = target;
            Value = value;
        }

        public override IEnumerable Children() {
            yield return Target;
            yield return Value;
        }
    }
}
