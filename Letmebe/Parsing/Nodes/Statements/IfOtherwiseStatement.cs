using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal class IfOtherwiseStatement : Statement {
        public readonly Token IfToken;
        public readonly Expression Condition;
        public readonly Statement ThenStatement;
        public readonly Token OtherwiseToken;
        public readonly Statement OtherwiseStatement;

        public IfOtherwiseStatement(Token ifToken, Expression condition, Statement thenStatement, Token otherwiseToken, Statement otherwiseStatement) {
            IfToken = ifToken;
            Condition = condition;
            ThenStatement = thenStatement;
            OtherwiseToken = otherwiseToken;
            OtherwiseStatement = otherwiseStatement;
        }

        public override IEnumerable Children() {
            yield return IfToken;
            yield return Condition;
            yield return ThenStatement;
            yield return OtherwiseToken;
            yield return OtherwiseStatement;
        }
    }
}
