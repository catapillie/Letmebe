using System.Diagnostics.CodeAnalysis;

namespace Letmebe.Binding.Nodes {
    internal abstract class BoundType {
        public static bool operator ==(BoundType a, BoundType b)
            => a.Equals(b);

        public static bool operator !=(BoundType a, BoundType b)
            => !a.Equals(b);

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
