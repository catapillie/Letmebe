using Letmebe.Binding.Nodes;

namespace Letmebe.Evaluation {
    internal sealed class EvalScope {
        private readonly EvalScope? ParentScope;

        public object? this[BoundSymbol symbol] {
            get => variables.TryGetValue(symbol, out var value) ? value : ParentScope?[symbol];
            set {
                if (variables.ContainsKey(symbol))
                    variables[symbol] = value;
                else if (ParentScope != null)
                    ParentScope[symbol] = value;
            }
        }

        public BoundStatement? this[BoundFunctionSymbol symbol] {
            get => functions.TryGetValue(symbol, out var body) ? body : ParentScope?[symbol];
        }

        private readonly Dictionary<BoundSymbol, object?> variables = new();
        private readonly Dictionary<BoundFunctionSymbol, BoundStatement> functions = new();

        public EvalScope(EvalScope? parentScope = null) {
            ParentScope = parentScope;
        }

        public void DeclareVariable(BoundSymbol symbol, object? value)
            => variables[symbol] = value;

        public void DefineFunction(BoundFunctionSymbol symbol, BoundStatement body)
            => functions[symbol] = body;

        public static EvalScope operator ++(EvalScope scope)
            => new(scope);

        public static EvalScope operator --(EvalScope scope)
            => scope.ParentScope ?? throw new Exception("Cannot go backwards from root scope");
    }
}
