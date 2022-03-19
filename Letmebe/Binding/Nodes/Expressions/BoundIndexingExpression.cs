namespace Letmebe.Binding.Nodes {
    internal sealed class BoundIndexingExpression : BoundExpression {
        public readonly BoundExpression Expression;
        public readonly BoundExpression IndexExpression;
        public readonly BoundIndexerOperator Operator;

        public override BoundType Type => Operator.ReturnType;

        public BoundIndexingExpression(BoundExpression expression, BoundExpression indexExpression, BoundIndexerOperator indexerOperator) {
            Expression = expression;
            IndexExpression = indexExpression;
            Operator = indexerOperator;
        }
    }
}
