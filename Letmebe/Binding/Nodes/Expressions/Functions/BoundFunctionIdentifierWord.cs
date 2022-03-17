namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionIdentifierWord : BoundFunctionWord {
        public readonly string Identifier;

        public BoundFunctionIdentifierWord(string identifier) {
            Identifier = identifier;
        }

        public override bool Equals(object? obj) {
            if (obj is BoundFunctionIdentifierWord other)
                return Identifier == other.Identifier;

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Identifier;
    }
}
