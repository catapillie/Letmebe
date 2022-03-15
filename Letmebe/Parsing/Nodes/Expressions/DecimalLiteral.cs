using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DecimalLiteral : Expression {
        public readonly Token FloatToken;

        public DecimalLiteral(Token token) {
            FloatToken = token;
        }

        public override IEnumerable Children() {
            yield return FloatToken;
        }
    }
}
