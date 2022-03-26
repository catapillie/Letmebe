namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionIdentifierWord : BoundFunctionWord {
        public readonly string Identifier;

        public BoundFunctionIdentifierWord(string identifier) {
            Identifier = identifier;
        }

        public override string ToString() => Identifier;
    }
}
