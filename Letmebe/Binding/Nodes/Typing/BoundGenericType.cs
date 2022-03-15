namespace Letmebe.Binding.Nodes {
    internal sealed class BoundGenericType : BoundType {
        public readonly BoundUserType UserType;
        public readonly BoundType[] InnerTypes;

        public BoundGenericType(BoundUserType userType, params BoundType[] innerType) {
            UserType = userType;
            InnerTypes = innerType;
        }

        public override bool Equals(BoundType other) {
            if (other is BoundGenericType generic)
                return UserType == generic.UserType && InnerTypes.SequenceEqual(generic.InnerTypes);

            return false;
        }

        public override string ToString()
            => UserType.ToString() + "<" + InnerTypes.Select(t => t.ToString()).Aggregate((a, b) => $"{a}, {b}") + ">";
    }
}
