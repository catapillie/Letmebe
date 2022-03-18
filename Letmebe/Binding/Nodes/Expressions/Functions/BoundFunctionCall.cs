using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionCall : BoundExpression {
        public readonly BoundFunctionSymbol Function;
        public readonly BoundExpression[] Parameters;

        public override BoundType Type => Function.ReturnType;

        public BoundFunctionCall(BoundFunctionSymbol function, BoundExpression[] parameters) {
            Function = function;
            Parameters = parameters;
        }

        public override IEnumerable Children() {
            yield return Function.Template + " -> " + (Function.ReturnType == null ? Function.ReturnType : "???");
            foreach (var parameter in Parameters)
                yield return parameter;
        }
    }
}
