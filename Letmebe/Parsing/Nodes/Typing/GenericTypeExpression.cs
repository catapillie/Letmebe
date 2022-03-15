using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class GenericTypeExpression : TypeExpression {
        public readonly Token Identifier;
        public readonly Token LeftChevronToken;
        public readonly TypeExpression[] TypeExpressions;
        public readonly Token RightChevronToken;

        public GenericTypeExpression(Token identifier, Token leftChevronToken, TypeExpression[] typeExpressions, Token rightChevronToken) {
            Identifier = identifier;
            LeftChevronToken = leftChevronToken;
            TypeExpressions = typeExpressions;
            RightChevronToken = rightChevronToken;
        }

        public override IEnumerable Children() {
            yield return Identifier;
            yield return LeftChevronToken;
            foreach (TypeExpression typeExpression in TypeExpressions)
                yield return typeExpression;
            yield return RightChevronToken;
        }
    }
}
