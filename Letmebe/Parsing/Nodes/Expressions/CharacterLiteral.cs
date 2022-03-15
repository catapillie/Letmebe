using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class CharacterLiteral : Expression {
        public readonly Token CharacterToken;

        public CharacterLiteral(Token token) {
            CharacterToken = token;
        }

        public override IEnumerable Children() {
            yield return CharacterToken;
        }
    }
}
