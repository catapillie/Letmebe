using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundIfOtherwiseStatement : BoundStatement {
        public readonly BoundExpression Condition;
        public readonly BoundStatement Statement, OtherwiseStatement;

        public BoundIfOtherwiseStatement(BoundExpression condition, BoundStatement statement, BoundStatement otherwiseStatement) {
            Condition = condition;
            Statement = statement;
            OtherwiseStatement = otherwiseStatement;
        }

        public override IEnumerable Children() {
            yield return Condition;
            yield return Statement;
            yield return OtherwiseStatement;
        }
    }
}
