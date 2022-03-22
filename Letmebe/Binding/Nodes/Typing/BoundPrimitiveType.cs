namespace Letmebe.Binding.Nodes {
    internal class BoundPrimitiveType : BoundType {
        public enum PrimitiveType {
            Object, Float, Integer, Boolean, String, Character, Type, Void,
        }

        public static readonly BoundPrimitiveType ObjectPrimitive       = new(PrimitiveType.Object);
        public static readonly BoundPrimitiveType FloatPrimitive        = new(PrimitiveType.Float);
        public static readonly BoundPrimitiveType IntegerPrimitive      = new(PrimitiveType.Integer) { Base = FloatPrimitive };
        public static readonly BoundPrimitiveType BooleanPrimitive      = new(PrimitiveType.Boolean);
        public static readonly BoundPrimitiveType StringPrimitive       = new(PrimitiveType.String);
        public static readonly BoundPrimitiveType CharacterPrimitive    = new(PrimitiveType.Character) { Base = StringPrimitive };
        public static readonly BoundPrimitiveType TypePrimitive         = new(PrimitiveType.Type);
        public static readonly BoundPrimitiveType VoidPrimitive         = new(PrimitiveType.Void);

        public readonly PrimitiveType Type;

        private BoundPrimitiveType(PrimitiveType type) {
            Type = type;
        }

        public override bool Is(BoundType other, bool inherit)
            => base.Is(other, inherit) || (other is BoundPrimitiveType primitive && Type == primitive.Type);

        public override string ToString()
            => Type switch {
                PrimitiveType.Integer => "int",
                PrimitiveType.Float => "float",
                PrimitiveType.Boolean => "bool",
                PrimitiveType.String => "str",
                PrimitiveType.Character => "char",
                PrimitiveType.Object => "obj",
                PrimitiveType.Type => "type",
                _ => "???"
            };
    }
}
