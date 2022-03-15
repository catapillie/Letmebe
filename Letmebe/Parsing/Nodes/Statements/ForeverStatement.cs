using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ForeverStatement : Statement {
        public readonly Token ForeverToken;
        public readonly Statement Statement;

        public ForeverStatement(Token foreverToken, Statement statement) {
            ForeverToken = foreverToken;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return ForeverToken;
            yield return Statement;
        }
    }
}
