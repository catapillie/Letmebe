namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionParameterWord : BoundFunctionWord {
        public readonly BoundType Type;

        public BoundFunctionParameterWord(BoundType type) {
            Type = type;
        }

        public override bool Equals(object? obj) {
            if (obj is BoundFunctionParameterWord other)
                return Type == other.Type;

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => "<" + Type.ToString() + ">";
    }
}
