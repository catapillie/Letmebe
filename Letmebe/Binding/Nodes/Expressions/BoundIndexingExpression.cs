namespace Letmebe.Binding.Nodes {
    internal sealed class BoundIndexingExpression : BoundExpression {
        public readonly BoundExpression Expression;
        public readonly BoundExpression IndexExpression;
        public readonly BoundIndexerOperator IndexerOperator;

        public override BoundType Type => IndexerOperator.ReturnType;

        public BoundIndexingExpression(BoundExpression expression, BoundExpression indexExpression, BoundIndexerOperator indexerOperator) {
            Expression = expression;
            IndexExpression = indexExpression;
            IndexerOperator = indexerOperator;
        }
    }
}
