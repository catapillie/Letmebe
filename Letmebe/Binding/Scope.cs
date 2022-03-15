using Letmebe.Binding.Nodes;

namespace Letmebe.Binding {
    internal sealed class Scope {
        private readonly Scope? ParentScope;

        private readonly Dictionary<(string, int), BoundUserType> types = new();
        private readonly Dictionary<string, BoundSymbol> variables = new();

        public Scope(Scope? parentScope = null) {
            ParentScope = parentScope;
        }

        public bool TryLookupType(string name, int genericArgumentCount, out BoundUserType boundType) {
            if (types.TryGetValue((name, genericArgumentCount), out boundType!))
                return true;
            else if (ParentScope is not null)
                return ParentScope.TryLookupType(name, genericArgumentCount, out boundType);

            boundType = new(name, genericArgumentCount);
            return false;
        }

        public bool TryRegisterType(string name, int genericArgumentCount) {
            if (types.ContainsKey((name, genericArgumentCount)))
                return false;

            types[(name, genericArgumentCount)] = new BoundUserType(name, genericArgumentCount);
            return true;
        }

        public bool TryLookupVariable(string name, out BoundSymbol boundType) {
            if (variables.TryGetValue(name, out boundType!))
                return true;
            else if (ParentScope is not null)
                return ParentScope.TryLookupVariable(name, out boundType);

            boundType = new(null!, name);
            return false;
        }

        public bool TryRegisterVariable(BoundType type, string name) {
            if (variables.ContainsKey(name))
                return false;

            variables[name] = new BoundSymbol(type, name);
            return true;
        }

        public static Scope operator ++(Scope scope)
            => new(scope);

        public static Scope operator --(Scope scope)
            => scope.ParentScope ?? throw new Exception("Cannot go backwards from root scope");
    }
}
