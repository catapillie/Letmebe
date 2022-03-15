using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class IndexingExpression : Expression {
        public readonly Expression Expression;
        public readonly Token LeftBracketToken;
        public readonly Expression IndexExpression;
        public readonly Token RightBracketToken;

        public IndexingExpression(Expression expression, Token leftBracketToken, Expression indexExpression, Token rightBracketToken) {
            Expression = expression;
            LeftBracketToken = leftBracketToken;
            IndexExpression = indexExpression;
            RightBracketToken = rightBracketToken;
        }

        public override IEnumerable Children() {
            yield return Expression;
            yield return LeftBracketToken;
            yield return IndexExpression;
            yield return RightBracketToken;
        }
    }
}
