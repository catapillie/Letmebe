using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class UnlessStatement : Statement {
        public readonly Token UnlessToken;
        public readonly Expression Condition;
        public readonly Statement ThenStatement;

        public UnlessStatement(Token unlessToken, Expression condition, Statement thenStatement) {
            UnlessToken = unlessToken;
            Condition = condition;
            ThenStatement = thenStatement;
        }

        public override IEnumerable Children() {
            yield return UnlessToken;
            yield return Condition;
            yield return ThenStatement;
        }
    }
}
