using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ParenthesizedTypeExpression : TypeExpression {
        public readonly Token LeftParenthesis;
        public readonly TypeExpression TypeExpression;
        public readonly Token RightParenthesis;

        public ParenthesizedTypeExpression(Token leftParenthesis, TypeExpression typeExpression, Token rightParenthesis) {
            LeftParenthesis = leftParenthesis;
            TypeExpression = typeExpression;
            RightParenthesis = rightParenthesis;
        }

        public override IEnumerable Children() {
            yield return LeftParenthesis;
            yield return TypeExpression;
            yield return RightParenthesis;
        }
    }
}
