using Letmebe.Binding.Nodes;

namespace Letmebe.Evaluation {
    internal sealed class EvalScope {
        private readonly EvalScope? ParentScope;

        public BoundType? ReturnType = null;

        public EvalScope(EvalScope? parentScope = null) {
            ParentScope = parentScope;
        }

        public static EvalScope operator ++(EvalScope scope)
            => new(scope);

        public static EvalScope operator --(EvalScope scope)
            => scope.ParentScope ?? throw new Exception("Cannot go backwards from root scope");
    }
}
