using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ExpressionStatement : Statement {
        public readonly Expression Expression;
        public readonly Token SemicolonToken;

        public readonly bool IsInvalid;

        public ExpressionStatement(Expression expression, Token semicolonToken) {
            Expression = expression;
            SemicolonToken = semicolonToken;

            IsInvalid = expression is not (FunctionCallExpression or AssignmentExpression);
        }

        public override IEnumerable Children() {
            yield return Expression;
            yield return SemicolonToken;
        }
    }
}
