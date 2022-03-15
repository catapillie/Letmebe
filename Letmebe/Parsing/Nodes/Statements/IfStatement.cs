using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class IfStatement : Statement {
        public readonly Token IfToken;
        public readonly Expression Condition;
        public readonly Statement ThenStatement;
        public readonly Token OtherwiseToken = null!;
        public readonly Statement OtherwiseStatement = null!;

        public readonly bool IsIfOtherwise;

        public IfStatement(Token ifToken, Expression condition, Statement thenStatement, Token otherwiseToken, Statement otherwiseStatement)
            : this(ifToken, condition, thenStatement) {
            OtherwiseToken = otherwiseToken;
            OtherwiseStatement = otherwiseStatement;
            IsIfOtherwise = true;
        }

        public IfStatement(Token ifToken, Expression condition, Statement thenStatement) {
            IfToken = ifToken;
            Condition = condition;
            ThenStatement = thenStatement;
            IsIfOtherwise = false;
        }

        public override IEnumerable Children() {
            yield return IfToken;
            yield return Condition;
            yield return ThenStatement;
            if (IsIfOtherwise) {
                yield return OtherwiseToken;
                yield return OtherwiseStatement;
            }
        }
    }
}
