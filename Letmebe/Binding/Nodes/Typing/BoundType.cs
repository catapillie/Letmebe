namespace Letmebe.Binding.Nodes {
    internal abstract class BoundType {
        public bool IsKnown => this != BoundUnknownType.Unknown;
        public bool MatchAny => this is BoundUnknownType t && t.MatchesAny;

        public BoundType Base = BoundPrimitiveType.ObjectPrimitive;

        public virtual bool Is(BoundType other, bool inherit = true)
            => inherit && Base != null && Base.Is(other);
    }
}
