using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class EmptyStatement : Statement {
        public readonly Token EmptyToken;

        public EmptyStatement(Token emptyToken) {
            EmptyToken = emptyToken;
        }

        public override IEnumerable Children() {
            yield return EmptyToken;
        }
    }
}
