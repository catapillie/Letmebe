using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class FunctionCallParameter : FunctionCallWord {
        public readonly Expression ParameterExpression;

        public FunctionCallParameter(Expression parameterExpression) {
            ParameterExpression = parameterExpression;
        }

        public override Expression ToExpression()
            => ParameterExpression;

        public override IEnumerable Children() {
            yield return ParameterExpression;
        }
    }
}
