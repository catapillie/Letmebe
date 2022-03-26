using Letmebe.Binding.Nodes;

namespace Letmebe.Binding {
    internal sealed class BuiltInFunctions {
        // print <obj> -> void
        public static readonly BoundFunctionSymbol PrintFunction = new(
            new(new BoundFunctionWord[] {
                new BoundFunctionIdentifierWord("print"),
                new BoundFunctionParameterWord(BoundPrimitiveType.ObjectPrimitive),
            }
        ), BoundPrimitiveType.VoidPrimitive);

        // ask <str> -> str
        public static readonly BoundFunctionSymbol AskFunction = new(
            new(new BoundFunctionWord[] {
                new BoundFunctionIdentifierWord("ask"),
                new BoundFunctionParameterWord(BoundPrimitiveType.StringPrimitive),
            }
        ), BoundPrimitiveType.StringPrimitive);
    }
}
