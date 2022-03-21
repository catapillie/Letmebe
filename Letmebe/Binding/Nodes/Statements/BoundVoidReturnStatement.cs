using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundVoidReturnStatement : BoundStatement {
        public readonly BoundFunctionSymbol ReturningFunction;

        public BoundVoidReturnStatement(BoundFunctionSymbol returningFunction) {
            ReturningFunction = returningFunction;
        }

        public override IEnumerable Children() {
            yield return ReturningFunction;
        }
    }
}
