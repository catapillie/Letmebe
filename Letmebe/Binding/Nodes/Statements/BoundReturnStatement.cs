using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundReturnStatement : BoundStatement {
        public readonly BoundExpression Expression;
        public readonly BoundFunctionSymbol ReturningFunction;

        public BoundReturnStatement(BoundExpression expression, BoundFunctionSymbol returningFunction) {
            Expression = expression;
            ReturningFunction = returningFunction;
        }

        public override IEnumerable Children() {
            yield return Expression;
            yield return ReturningFunction;
        }
    }
}
