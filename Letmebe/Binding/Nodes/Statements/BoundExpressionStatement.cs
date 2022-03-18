using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundExpressionStatement : BoundStatement {
        public readonly BoundExpression Expression;

        public BoundExpressionStatement(BoundExpression expression) {
            Expression = expression;
        }

        public override IEnumerable Children() {
            yield return Expression;
        }
    }
}
