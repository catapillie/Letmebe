using Letmebe.Binding.Nodes;

namespace Letmebe.Evaluation {
    internal sealed class EvalScope {
        private readonly EvalScope? ParentScope;

        public object? this[BoundSymbol symbol] {
            get => variables.TryGetValue(symbol, out var value) ? value : ParentScope?[symbol];
            set => variables[symbol] = value;
        }

        private readonly Dictionary<BoundSymbol, object?> variables = new();

        public EvalScope(EvalScope? parentScope = null) {
            ParentScope = parentScope;
        }

        public static EvalScope operator ++(EvalScope scope)
            => new(scope);

        public static EvalScope operator --(EvalScope scope)
            => scope.ParentScope ?? throw new Exception("Cannot go backwards from root scope");
    }
}
