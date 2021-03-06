using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class IfStatement : Statement {
        public readonly Token IfToken;
        public readonly Expression Condition;
        public readonly Statement ThenStatement;

        public IfStatement(Token ifToken, Expression condition, Statement thenStatement) {
            IfToken = ifToken;
            Condition = condition;
            ThenStatement = thenStatement;
        }

        public override IEnumerable Children() {
            yield return IfToken;
            yield return Condition;
            yield return ThenStatement;
        }
    }
}
