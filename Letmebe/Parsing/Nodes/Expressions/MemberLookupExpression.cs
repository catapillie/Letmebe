using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class MemberLookupExpression : Expression {
        public readonly Expression Expression;
        public readonly Token DotToken;
        public readonly Token MemberIdentifierToken;

        public MemberLookupExpression(Expression expression, Token dotToken, Token memberIdentifierToken) {
            Expression = expression;
            DotToken = dotToken;
            MemberIdentifierToken = memberIdentifierToken;
        }

        public override IEnumerable Children() {
            yield return Expression;
            yield return DotToken;
            yield return MemberIdentifierToken;
        }
    }
}
