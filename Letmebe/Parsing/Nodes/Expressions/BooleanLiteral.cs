using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class BooleanLiteral : Expression {
        public readonly Token BooleanToken;

        public BooleanLiteral(Token token) {
            BooleanToken = token;
        }

        public override IEnumerable Children() {
            yield return BooleanToken;
        }
    }
}
