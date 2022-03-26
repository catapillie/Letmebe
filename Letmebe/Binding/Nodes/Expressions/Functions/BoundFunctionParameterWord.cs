namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionParameterWord : BoundFunctionWord {
        public readonly BoundType Type;

        public BoundFunctionParameterWord(BoundType type) {
            Type = type;
        }

        public override string ToString() => "<" + Type.ToString() + ">";
    }
}
