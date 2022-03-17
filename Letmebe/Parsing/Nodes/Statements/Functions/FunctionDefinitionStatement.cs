using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class FunctionDefinitionStatement : Statement {
        public readonly Token LetToken;
        public readonly DefinitionSentence Sentence;
        public readonly Token BeToken;
        public readonly TypeExpression TypeExpression;
        public readonly Statement Body;

        public FunctionDefinitionStatement(Token letToken, DefinitionSentence sentence, Token beToken, TypeExpression typeExpression, Statement body) {
            LetToken = letToken;
            Sentence = sentence;
            BeToken = beToken;
            TypeExpression = typeExpression;
            Body = body;
        }

        public override IEnumerable Children() {
            yield return LetToken;
            yield return Sentence;
            yield return BeToken;
            yield return TypeExpression;
            yield return Body;
        }
    }
}
