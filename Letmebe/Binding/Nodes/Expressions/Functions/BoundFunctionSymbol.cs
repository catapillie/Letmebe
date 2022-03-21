namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionSymbol {
        public readonly BoundFunctionTemplate Template;
        public BoundSymbol[] ParameterSymbols = null!;
        public readonly BoundType ReturnType;

        public BoundFunctionSymbol(BoundFunctionTemplate template, BoundType returnType) {
            Template = template;
            ReturnType = returnType;
        }

        public override string ToString()
            => $"{Template} -> {ReturnType}";
    }
}
