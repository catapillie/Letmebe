using Letmebe.Lexing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letmebe.Parsing.Nodes {
    internal sealed class TupleTypeExpression : TypeExpression {
        public readonly Token LeftParenthesisToken;
        public readonly TypeExpression[] TypeExpressions;
        public readonly Token RightParenthesisToken;

        public TupleTypeExpression(Token leftParenthesisToken, TypeExpression[] typeExpressions, Token rightParenthesisToken) {
            LeftParenthesisToken = leftParenthesisToken;
            TypeExpressions = typeExpressions;
            RightParenthesisToken = rightParenthesisToken;
        }

        public override IEnumerable Children() {
            yield return LeftParenthesisToken;
            foreach (TypeExpression typeExpression in TypeExpressions)
                yield return typeExpression;
            yield return RightParenthesisToken;
        }
    }
}
