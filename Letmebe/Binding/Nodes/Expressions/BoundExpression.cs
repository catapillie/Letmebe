namespace Letmebe.Binding.Nodes {
    internal abstract class BoundExpression : BoundNode {
        public abstract BoundType Type { get; }
    }
}
