namespace Letmebe.Binding.Nodes {
    internal sealed class BoundArrayType : BoundType {
        public readonly BoundType Type;
        public BoundArrayType(BoundType type) {
            Type = type;
        }

        public override bool Is(BoundType other, bool inherit)
            => base.Is(other, inherit) || (other is BoundArrayType array && Type.Is(array.Type, inherit));

        public override string ToString()
            => "[" + Type.ToString() + "]";
    }
}
