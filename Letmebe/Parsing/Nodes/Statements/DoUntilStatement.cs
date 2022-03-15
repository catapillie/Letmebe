using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DoUntilStatement : DoStatement {
        public readonly Token UntilToken;
        public readonly Expression Condition;
        public readonly Token SemicolonToken;

        public DoUntilStatement(Token doToken, Statement statement, Token untilToken, Expression condition, Token semicolonToken)
            : base(doToken, statement) {
            UntilToken = untilToken;
            Condition = condition;
            SemicolonToken = semicolonToken;
        }

        public override IEnumerable Children() {
            yield return DoToken;
            yield return Statement;
            yield return UntilToken;
            yield return Condition;
            yield return SemicolonToken;
        }
    }
}
