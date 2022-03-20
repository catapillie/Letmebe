using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundLabelStatement : BoundStatement {
        public readonly string Name;

        public BoundLabelStatement(string name) {
            Name = name;
        }

        public override IEnumerable Children() {
            yield return Name;
        }
    }
}
