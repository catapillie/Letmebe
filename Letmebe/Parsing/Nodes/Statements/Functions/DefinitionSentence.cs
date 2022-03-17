using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DefinitionSentence : SyntaxNode {
        public readonly DefinitionWord[] Words;
        public readonly bool IsIdentifier;

        public DefinitionSentence(DefinitionWord[] words) {
            Words = words;
            IsIdentifier = words.Length == 1 && words[0] is DefinitionIdentifierWord;
        }

        public override IEnumerable Children() {
            foreach (DefinitionWord word in Words)
                yield return word;
        }

        public static explicit operator Token(DefinitionSentence sentence)
            => (sentence.Words[0] as DefinitionIdentifierWord)!.IdentifierToken;
    }
}
