using Letmebe.Binding.Nodes;

namespace Letmebe.Binding {
    internal sealed class Scope {
        private readonly Scope? ParentScope;

        private readonly Dictionary<(string, int), BoundUserType> types = new();
        private readonly Dictionary<string, BoundSymbol> variables = new();
        private readonly Dictionary<BoundFunctionTemplate, BoundFunctionSymbol> functions = new();

        public Scope(Scope? parentScope = null) {
            ParentScope = parentScope;
        }

        public bool TryLookupType(string name, int genericArgumentCount, out BoundUserType type) {
            if (types.TryGetValue((name, genericArgumentCount), out type!))
                return true;
            else if (ParentScope is not null)
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
            else if (ParentScope is not null)
                return ParentScope.TryLookupVariable(name, out symbol);

            symbol = new(BoundType.Unknown, name);
            return false;
        }

        public bool TryRegisterVariable(BoundType type, string name, out BoundSymbol symbol) {
            if (variables.ContainsKey(name)) {
                symbol = variables[name];
                return false;
            }

            variables[name] = symbol = new(type, name);
            return true;
        }

        public bool TryLookupFunction(BoundFunctionTemplate template, out BoundFunctionSymbol function) {
            if (functions.TryGetValue(template, out function!))
                return true;
            else if (ParentScope is not null)
                return ParentScope.TryLookupFunction(template, out function);

            function = new(template, BoundType.Unknown);
            return false;
        }

        public bool TryRegisterFunction(BoundType type, BoundFunctionTemplate template) {
            if (functions.ContainsKey(template))
                return false;

            functions[template] = new(template, type);
            return true;
        }

        public static Scope operator ++(Scope scope)
            => new(scope);

        public static Scope operator --(Scope scope)
            => scope.ParentScope ?? throw new Exception("Cannot go backwards from root scope");
    }
}
