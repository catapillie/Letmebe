using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DefinitionIdentifierWord : DefinitionWord {
        public readonly Token IdentifierToken;

        public DefinitionIdentifierWord(Token identifierToken) {
            IdentifierToken = identifierToken;
        }

        public override IEnumerable Children() {
            yield return IdentifierToken;
        }
    }
}
