using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ExpressionStatament : Statement {
        public readonly Expression Expression;
        public readonly Token SemicolonToken;

        public ExpressionStatament(Expression expression, Token semicolonToken) {
            Expression = expression;
            SemicolonToken = semicolonToken;
        }

        public override IEnumerable Children() {
            yield return Expression;
            yield return SemicolonToken;
        }
    }
}
