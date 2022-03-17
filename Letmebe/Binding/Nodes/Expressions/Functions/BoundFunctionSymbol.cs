namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionSymbol {
        public readonly BoundFunctionTemplate Template;
        public readonly BoundType ReturnType;

        public BoundFunctionSymbol(BoundFunctionTemplate template, BoundType returnType) {
            Template = template;
            ReturnType = returnType;
        }
    }
}
