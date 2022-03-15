using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DoWhileStatement : DoStatement {
        public readonly Token WhileToken;
        public readonly Expression Condition;
        public readonly Token SemicolonToken;

        public DoWhileStatement(Token doToken, Statement statement, Token whileToken, Expression condition, Token semicolonToken)
            : base(doToken, statement) {
            WhileToken = whileToken;
            Condition = condition;
            SemicolonToken = semicolonToken;
        }

        public override IEnumerable Children() {
            yield return DoToken;
            yield return Statement;
            yield return WhileToken;
            yield return Condition;
            yield return SemicolonToken;
        }
    }
}
