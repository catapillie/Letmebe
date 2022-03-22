using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundArrayExpression : BoundExpression {
        private readonly BoundType type;
        public readonly BoundExpression[] Values;

        public override BoundType Type => type;

        public BoundArrayExpression(BoundExpression[] values, BoundType type) {
            Values = values;
            this.type = new BoundArrayType(type);
        }

        public override IEnumerable Children() {
            foreach (var value in Values)
                yield return value;
        }
    }
}
