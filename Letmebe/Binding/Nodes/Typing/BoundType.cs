namespace Letmebe.Binding.Nodes {
    internal abstract class BoundType {
        public bool IsKnown => this != BoundUnknownType.Unknown;
        public bool MatchAny => this is BoundUnknownType t && t.MatchesAny;

        public virtual bool Is(BoundType other, bool inherit = true)
            => this == other || MatchAny;
    }
}
