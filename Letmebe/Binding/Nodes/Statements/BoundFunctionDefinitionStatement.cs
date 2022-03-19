using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionDefinitionStatement : BoundStatement {
        public readonly BoundFunctionSymbol Symbol;
        public readonly BoundStatement Body;

        public BoundFunctionDefinitionStatement(BoundFunctionSymbol symbol, BoundStatement body) {
            Symbol = symbol;
            Body = body;
        }

        public override IEnumerable Children() {
            yield return Symbol;
            yield return Body;
        }
    }
}
