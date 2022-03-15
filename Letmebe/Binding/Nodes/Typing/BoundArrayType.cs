namespace Letmebe.Binding.Nodes {
    internal sealed class BoundArrayType : BoundType {
        public readonly BoundType Type;
        public BoundArrayType(BoundType type) {
            Type = type;
        }

        public override bool Equals(BoundType other) {
            if (other is BoundArrayType array)
                return Type.Equals(array.Type);

            return false;
        }

        public override string ToString()
            => "[" + Type.ToString() + "]";
    }
}
