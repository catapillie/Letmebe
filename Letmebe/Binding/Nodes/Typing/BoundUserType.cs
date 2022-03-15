namespace Letmebe.Binding.Nodes {
    internal sealed class BoundUserType : BoundType {
        public readonly string Name;
        public readonly int GenericArgumentCount;

        public BoundUserType(string name, int genericArgumentCount) {
            Name = name;
            GenericArgumentCount = genericArgumentCount;
        }

        public override bool Equals(BoundType other)
            => ReferenceEquals(this, other);

        public override string ToString()
            => Name;
    }
}
