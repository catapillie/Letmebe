using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ArrayExpression : Expression {
        public readonly Token LeftBracketToken, RightBracketToken;
        public readonly Expression[] Values;

        public ArrayExpression(Token leftBracketToken, Expression[] values, Token rightBracketToken) {
            LeftBracketToken = leftBracketToken;
            Values = values;
            RightBracketToken = rightBracketToken;
        }

        public override IEnumerable Children() {
            yield return LeftBracketToken;
            foreach (var value in Values)
                yield return value;
            yield return RightBracketToken;
        }
    }
}
