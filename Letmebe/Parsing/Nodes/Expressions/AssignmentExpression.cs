using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class AssignmentExpression : Expression {
        public readonly Expression Target;
        public readonly Token LeftArrowToken;
        public readonly Expression Value;

        public AssignmentExpression(Expression target, Token leftArrowToken, Expression value) {
            Target = target;
            LeftArrowToken = leftArrowToken;
            Value = value;
        }

        public override IEnumerable Children() {
            yield return Target;
            yield return LeftArrowToken;
            yield return Value;
        }
    }
}
