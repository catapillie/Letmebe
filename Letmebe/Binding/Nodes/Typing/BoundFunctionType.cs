namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionType : BoundType {
        public readonly BoundType ParameterType, ReturnType;

        public BoundFunctionType(BoundType parameterType, BoundType returnType) {
            ParameterType = parameterType;
            ReturnType = returnType;
        }

        public override bool Equals(BoundType other) {
            if (other is BoundFunctionType function)
                return ParameterType == function.ParameterType && ReturnType == function.ReturnType;

            return false;
        }

        public override string ToString() 
            => ParameterType.ToString() + " -> " + ReturnType.ToString();
    }
}
