using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundSymbol : BoundExpression {
        private readonly BoundType type;
        public readonly string Name;

        public override BoundType Type => type;

        public BoundSymbol(BoundType type, string name) {
            this.type = type;
            Name = name;
        }

        public override IEnumerable Children() {
            yield return Name;
        }
    }
}
