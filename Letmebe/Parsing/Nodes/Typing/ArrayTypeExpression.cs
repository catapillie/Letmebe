using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ArrayTypeExpression : TypeExpression {
        public readonly Token LeftBracketToken;
        public readonly TypeExpression TypeExpression;
        public readonly Token RightBracketToken;

        public ArrayTypeExpression(Token leftBracketToken, TypeExpression typeExpression, Token rightBracketToken) {
            LeftBracketToken = leftBracketToken;
            TypeExpression = typeExpression;
            RightBracketToken = rightBracketToken;
        }

        public override IEnumerable Children() {
            yield return LeftBracketToken;
            yield return TypeExpression;
            yield return RightBracketToken;
        }
    }
}
