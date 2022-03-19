using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DoUntilStatement : Statement {
        public readonly Token DoToken;
        public readonly Statement Statement;
        public readonly Token UntilToken;
        public readonly Expression Condition;
        public readonly Token SemicolonToken;

        public DoUntilStatement(Token doToken, Statement statement, Token untilToken, Expression condition, Token semicolonToken) {
            DoToken = doToken;
            Statement = statement;
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
