using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ParenthesizedExpression : Expression {
        public readonly Token LeftParenthesis;
        public readonly Expression Expression;
        public readonly Token RightParenthesis;

        public ParenthesizedExpression(Token leftParenthesis, Expression expression, Token rightParenthesis) {
            LeftParenthesis = leftParenthesis;
            Expression = expression;
            RightParenthesis = rightParenthesis;
        }

        public override IEnumerable Children() {
            yield return LeftParenthesis;
            yield return Expression;
            yield return RightParenthesis;
        }
    }
}
