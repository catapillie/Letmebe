namespace Letmebe.Binding.Nodes {
    internal sealed class BoundGenericType : BoundType {
        public readonly BoundUserType UserType;
        public readonly BoundType[] InnerTypes;

        public BoundGenericType(BoundUserType userType, params BoundType[] innerType) {
            UserType = userType;
            InnerTypes = innerType;
        }

        public override bool Is(BoundType other, bool inherit) {
            if (other is BoundGenericType generic) {
                if (InnerTypes.Length == generic.InnerTypes.Length) {
                    for (int i = 0; i < InnerTypes.Length; i++)
                        if (!InnerTypes[i].Is(generic.InnerTypes[i], inherit))
                            return base.Is(other, inherit);
                    return true;
                }
            }
            return base.Is(other, inherit);
        }

        public override string ToString()
            => UserType.ToString() + "<" + InnerTypes.Select(t => t.ToString()).Aggregate((a, b) => $"{a}, {b}") + ">";
    }
}
