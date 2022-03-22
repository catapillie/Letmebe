namespace Letmebe.Binding.Nodes {
    internal sealed class BoundUnknownType : BoundType {
        public static readonly BoundUnknownType
            Unknown = new(false),
            Matching = new(true);

        public readonly bool MatchesAny;

        private BoundUnknownType(bool matchAny) {
            MatchesAny = matchAny;
        }

        public override bool Equals(BoundType other)
            => ReferenceEquals(this, other);

        public override string ToString()
            => MatchesAny ? "any" : "???";
    }
}
