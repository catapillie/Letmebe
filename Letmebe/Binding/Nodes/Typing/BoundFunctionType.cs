namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionType : BoundType {
        public readonly BoundType ParameterType, ReturnType;

        public BoundFunctionType(BoundType parameterType, BoundType returnType) {
            ParameterType = parameterType;
            ReturnType = returnType;
        }

        public override bool Is(BoundType other, bool inherit)
            => base.Is(other, inherit) || (other is BoundFunctionType function && ParameterType.Is(function.ParameterType, inherit) && ReturnType.Is(function.ReturnType, inherit));

        public override string ToString() 
            => ParameterType.ToString() + " -> " + ReturnType.ToString();
    }
}
