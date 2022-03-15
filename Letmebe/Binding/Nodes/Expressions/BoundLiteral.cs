using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundLiteral : BoundExpression {
        private readonly BoundPrimitiveType type;
        public readonly object Value;

        public override BoundType Type => type;

        public BoundLiteral(BoundPrimitiveType type, object value) {
            this.type = type;
            Value = value;
        }

        public override IEnumerable Children() {
            yield return Value;
        }
    }
}
