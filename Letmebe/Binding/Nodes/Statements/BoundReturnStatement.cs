using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundReturnStatement : BoundStatement {
        public readonly BoundExpression Expression;

        public BoundReturnStatement(BoundExpression expression) {
            Expression = expression;
        }

        public override IEnumerable Children() {
            yield return Expression;
        }
    }
}
