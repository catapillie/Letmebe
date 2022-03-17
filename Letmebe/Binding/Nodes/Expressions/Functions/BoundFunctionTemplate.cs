namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionTemplate {
        public readonly BoundFunctionWord[] Words;

        public BoundFunctionTemplate(BoundFunctionWord[] words) {
            Words = words;
        }

        public override bool Equals(object? obj) {
            if (obj is BoundFunctionTemplate other)
                return Words.SequenceEqual(other.Words);

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString()
            => Words.Select(word => word.ToString()).Aggregate((a, b) => a + " " + b) ?? string.Empty;
    }
}
