using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class StringLiteral : Expression {
        public readonly Token StringToken;

        public StringLiteral(Token token) {
            StringToken = token;
        }

        public override IEnumerable Children() {
            yield return StringToken;
        }
    }
}
