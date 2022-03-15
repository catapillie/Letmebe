using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class FunctionTypeExpression : TypeExpression {
        public readonly TypeExpression ParameterTypeExpression;
        public readonly Token ArrowToken;
        public readonly TypeExpression ReturnTypeExpression;

        public FunctionTypeExpression(TypeExpression parameterTypeExpression, Token arrowToken, TypeExpression returnTypeExpression) {
            ParameterTypeExpression = parameterTypeExpression;
            ArrowToken = arrowToken;
            ReturnTypeExpression = returnTypeExpression;
        }

        public override IEnumerable Children() {
            yield return ParameterTypeExpression;
            yield return ArrowToken;
            yield return ReturnTypeExpression;
        }
    }
}
