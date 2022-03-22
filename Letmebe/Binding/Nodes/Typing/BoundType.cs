using System.Diagnostics.CodeAnalysis;

namespace Letmebe.Binding.Nodes {
    internal abstract class BoundType {
        public bool IsKnown => this != BoundUnknownType.Unknown;
        public bool MatchAny => this is BoundUnknownType t && t.MatchesAny;

        public static bool operator ==(BoundType? a, BoundType? b)
            => a is not null && b is not null && a.Equals(b);

        public static bool operator !=(BoundType? a, BoundType? b)
            => a is null || b is null || !a.Equals(b);

        public abstract bool Equals(BoundType other);

        public override bool Equals([NotNullWhen(true)] object? obj) {
            if (obj is BoundType boundType)
                return Equals(boundType);
            return false;
        }

        public override int GetHashCode()
            => base.GetHashCode();
    }
}
