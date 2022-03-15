using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class UntilStatement : Statement {
        public readonly Token UntilToken;
        public readonly Expression Condition;
        public readonly Statement Statement;

        public UntilStatement(Token untilToken, Expression condition, Statement statement) {
            UntilToken = untilToken;
            Condition = condition;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return UntilToken;
            yield return Condition;
            yield return Statement;
        }
    }
}
