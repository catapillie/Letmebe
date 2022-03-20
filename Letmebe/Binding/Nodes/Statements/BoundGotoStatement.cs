using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundGotoStatement : BoundStatement {
        public readonly BoundLabelStatement Label;

        public BoundGotoStatement(BoundLabelStatement label) {
            Label = label;
        }

        public override IEnumerable Children() {
            yield return "~> " + Label.Name;
        }
    }
}
