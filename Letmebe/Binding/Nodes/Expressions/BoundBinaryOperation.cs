using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundBinaryOperation : BoundExpression {
        public readonly BoundExpression Left;
        public readonly BoundBinaryOperator Operator;
        public readonly BoundExpression Right;

        public override BoundType Type => Operator.ReturnType;

        public BoundBinaryOperation(BoundExpression left, BoundBinaryOperator op, BoundExpression right) {
            Left = left;
            Operator = op;
            Right = right;
        }

        public override IEnumerable Children() {
            yield return Left;
            yield return Operator.OperatorToken;
            yield return Right;
        }
    }
}
