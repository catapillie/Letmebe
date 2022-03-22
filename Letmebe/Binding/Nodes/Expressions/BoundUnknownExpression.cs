namespace Letmebe.Binding.Nodes {
    internal sealed class BoundUnknownExpression : BoundExpression {
        public override BoundType Type => BoundUnknownType.Unknown; 
    }
}