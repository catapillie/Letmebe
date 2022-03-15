using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class VariableLiteral : Expression {
        public readonly Token IdentifierToken;

        public VariableLiteral(Token identifierToken) {
            IdentifierToken = identifierToken;
        }

        public override IEnumerable Children() {
            yield return IdentifierToken;
        }
    }
}
