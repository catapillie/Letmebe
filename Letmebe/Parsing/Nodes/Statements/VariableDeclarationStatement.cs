using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class VariableDeclarationStatement : Statement {
        public readonly Token LetToken;
        public readonly Token Identifier;
        public readonly Token BeToken;
        public readonly TypeExpression TypeExpression;
        public readonly Expression Expression;
        public readonly Token SemicolonToken;

        public VariableDeclarationStatement(Token letToken, Token identifier, Token beToken, TypeExpression typeExpression, Expression expression, Token semicolonToken) {
            LetToken = letToken;
            Identifier = identifier;
            BeToken = beToken;
            TypeExpression = typeExpression;
            Expression = expression;
            SemicolonToken = semicolonToken;
        }

        public override IEnumerable Children() {
            yield return LetToken;
            yield return Identifier;
            yield return BeToken;
            yield return TypeExpression;
            yield return Expression;
            yield return SemicolonToken;
        }
    }
}
