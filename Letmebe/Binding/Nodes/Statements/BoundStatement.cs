namespace Letmebe.Binding.Nodes {
    internal abstract class BoundStatement : BoundNode {
        public virtual BoundStatement Lowered() => this;
    }
}
