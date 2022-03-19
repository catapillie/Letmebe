using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DoWhileStatement : Statement {
        public readonly Token DoToken;
        public readonly Statement Statement;
        public readonly Token WhileToken;
        public readonly Expression Condition;
        public readonly Token SemicolonToken;

        public DoWhileStatement(Token doToken, Statement statement, Token whileToken, Expression condition, Token semicolonToken) {
            DoToken = doToken;
            Statement = statement;
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
