using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DoStatement : Statement {
        public readonly Token DoToken;
        public readonly Statement Statement;

        public DoStatement(Token doToken, Statement statement) {
            DoToken = doToken;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return DoToken;
            yield return Statement;
        }
    }
}
