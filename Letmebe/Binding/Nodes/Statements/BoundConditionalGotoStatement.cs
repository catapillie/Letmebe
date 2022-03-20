using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundConditionalGotoStatement : BoundStatement {
        public readonly BoundLabelStatement Label;
        public readonly BoundExpression Condition;
        public readonly bool Negated;

        public BoundConditionalGotoStatement(BoundLabelStatement label, BoundExpression condition, bool negated) {
            Label = label;
            Condition = condition;
            Negated = negated;
        }

        public override IEnumerable Children() {
            yield return "~> " + Label.Name + (Negated ? " if not" : " if");
            yield return Condition;
        }
    }
}
