using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundVariableDefinitionStatement : BoundStatement {
        public readonly BoundSymbol Variable;
        public readonly BoundExpression Value;

        public BoundVariableDefinitionStatement(BoundSymbol variable, BoundExpression value) {
            Variable = variable;
            Value = value;
        }

        public override IEnumerable Children() {
            yield return Variable;
            yield return Value;
        }
    }
}
