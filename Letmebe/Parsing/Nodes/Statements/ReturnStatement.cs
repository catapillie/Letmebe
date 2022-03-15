using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ReturnStatement : Statement {
        public readonly Token ReturnToken;
        public readonly Expression? Expression;
        public readonly Token SemicolonToken;

        public readonly bool HasExpression;

        public ReturnStatement(Token returnToken, Expression expression, Token semicolonToken) {
            ReturnToken = returnToken;
            Expression = expression;
            SemicolonToken = semicolonToken;
            HasExpression = true;
        }

        public ReturnStatement(Token returnToken, Token semicolonToken) {
            ReturnToken = returnToken;
            SemicolonToken = semicolonToken;
            HasExpression = false;
        }

        public override IEnumerable Children() {
            yield return ReturnToken;
            if (Expression is not null)
                yield return Expression;
            yield return SemicolonToken;
        }
    }
}
