using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundUnaryOperation : BoundExpression {
        public readonly BoundExpression Operand;
        public readonly BoundUnaryOperator Operator;

        public override BoundType Type => Operator.ReturnType;

        public BoundUnaryOperation(BoundExpression operand, BoundUnaryOperator op) {
            Operand = operand;
            Operator = op;
        }

        public override IEnumerable Children() {
            yield return Operator.OperatorToken;
            yield return Operand;
        }
    }
}
