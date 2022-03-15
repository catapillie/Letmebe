using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class DefinitionParameterListWord : DefinitionWord {
        public readonly Token LeftChevronToken;
        public readonly (TypeExpression TypeExpression, Token IdentifierToken)[] Parameters;
        public readonly Token RightChevronToken;

        public DefinitionParameterListWord(Token leftChevronToken, (TypeExpression TypeExpression, Token IdentifierToken)[] parameters, Token rightChevronToken) {
            LeftChevronToken = leftChevronToken;
            Parameters = parameters;
            RightChevronToken = rightChevronToken;
        }

        public override IEnumerable Children() {
            yield return LeftChevronToken;
            foreach (var (TypeExpression, IdentifierToken) in Parameters) {
                yield return TypeExpression;
                yield return IdentifierToken;
            }
            yield return RightChevronToken;
        }
    }
}
