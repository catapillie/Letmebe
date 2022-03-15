using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal class VoidFunctionDefinitionStatement : Statement {
        public readonly Token LetToken;
        public readonly DefinitionSentence Sentence;
        public readonly Token DoToken;
        public readonly Statement Body;

        public VoidFunctionDefinitionStatement(Token letToken, DefinitionSentence sentence, Token beToken, Statement body) {
            LetToken = letToken;
            Sentence = sentence;
            DoToken = beToken;
            Body = body;
        }

        public override IEnumerable Children() {
            yield return LetToken;
            yield return Sentence;
            yield return DoToken;
            yield return Body;
        }
    }
}
