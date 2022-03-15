using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class FunctionCallIdentifier : FunctionCallWord {
        public readonly VariableLiteral Variable;

        public FunctionCallIdentifier(VariableLiteral variable) {
            Variable = variable;
        }

        public override Expression ToExpression()
            => Variable;

        public override IEnumerable Children() {
            yield return Variable;
        }
    }
}
