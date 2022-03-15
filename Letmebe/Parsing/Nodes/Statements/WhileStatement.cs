using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class WhileStatement : Statement {
        public readonly Token WhileToken;
        public readonly Expression Condition;
        public readonly Statement Statement;

        public WhileStatement(Token whileToken, Expression condition, Statement statement) {
            WhileToken = whileToken;
            Condition = condition;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return WhileToken;
            yield return Condition;
            yield return Statement;
        }
    }
}
