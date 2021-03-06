using Letmebe.Binding.Nodes;

namespace Letmebe.Binding {
    internal sealed class Scope {
        private readonly Scope? ParentScope;

        private readonly Dictionary<(string, int), BoundUserType> types = new();
        private readonly Dictionary<string, BoundSymbol> variables = new();
        private readonly Dictionary<BoundFunctionTemplate, BoundFunctionSymbol> functions = new();

        public BoundType? ReturnType = null;
        public BoundFunctionSymbol? ReturningFunction = null;

        public Scope(Scope? parentScope = null) {
            ParentScope = parentScope;
            if (parentScope != null) {
                ReturnType = parentScope.ReturnType;
                ReturningFunction = parentScope.ReturningFunction;
            }
        }

        public bool TryLookupType(string name, int genericArgumentCount, out BoundUserType type) {
            if (types.TryGetValue((name, genericArgumentCount), out type!))
                return true;
            else if (ParentScope != null)
                return ParentScope.TryLookupType(name, genericArgumentCount, out type);

            type = new(name, genericArgumentCount);
            return false;
        }

        public bool TryRegisterType(string name, int genericArgumentCount) {
            if (types.ContainsKey((name, genericArgumentCount)))
                return false;

            types[(name, genericArgumentCount)] = new(name, genericArgumentCount);
            return true;
        }

        public bool TryLookupVariable(string name, out BoundSymbol symbol) {
            if (variables.TryGetValue(name, out symbol!))
                return true;
            else if (ParentScope != null)
                return ParentScope.TryLookupVariable(name, out symbol);

            symbol = new(BoundUnknownType.Unknown, name);
            return false;
        }

        public bool TryRegisterVariable(BoundType type, string name, out BoundSymbol symbol) {
            if (variables.TryGetValue(name, out symbol!))
                return false;

            variables[name] = symbol = new(type, name);
            return true;
        }

        public bool TryLookupFunction(BoundFunctionTemplate template, out BoundFunctionSymbol function) {
            function = functions.FirstOrDefault(pair => template.Matches(pair.Key)).Value;
            if (function != null)
                return true;
            else if (ParentScope != null)
                return ParentScope.TryLookupFunction(template, out function);

            function = new(template, BoundUnknownType.Unknown);
            return false;
        }

        public bool TryRegisterFunction(BoundType type, BoundFunctionTemplate template, out BoundFunctionSymbol symbol) {
            symbol = functions.FirstOrDefault(pair => template.Matches(pair.Key)).Value;
            if (symbol != null)
                return false;

            functions[template] = symbol = new(template, type);
            return true;
        }

        public void RegisterBuiltInFunction(BoundFunctionSymbol function)
            => functions[function.Template] = function;

        public static Scope operator ++(Scope scope)
            => new(scope);

        public static Scope operator --(Scope scope)
            => scope.ParentScope ?? throw new Exception("Cannot go backwards from root scope");
    }
}
