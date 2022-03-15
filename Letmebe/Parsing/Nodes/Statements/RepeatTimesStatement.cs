using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class RepeatTimesStatement : Statement {
        public readonly Token RepeatToken;
        public readonly Expression Amount;
        public readonly Token TimesToken;
        public readonly Statement Statement;

        public RepeatTimesStatement(Token repeatToken, Expression amount, Token timesToken, Statement statement) {
            RepeatToken = repeatToken;
            Amount = amount;
            TimesToken = timesToken;
            Statement = statement;
        }

        public override IEnumerable Children() {
            yield return RepeatToken;
            yield return Amount;
            yield return TimesToken;
            yield return Statement;
        }
    }
}
