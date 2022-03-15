using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class IntegerLiteral : Expression {
        public readonly Token IntegerToken;

        public IntegerLiteral(Token token) {
            IntegerToken = token;
        }

        public override IEnumerable Children() {
            yield return IntegerToken;
        }
    }
}
