namespace Letmebe.Binding.Nodes {
    internal sealed class BoundTupleType : BoundType {
        public readonly BoundType[] Types;

        public BoundTupleType(params BoundType[] types) {
            Types = types;
        }

        public override bool Equals(BoundType other) {
            if (other is BoundTupleType tuple)
                return Types.SequenceEqual(tuple.Types);
            
            return false;
        }

        public override string ToString()
            => "(" + Types.Select(t => t.ToString()).Aggregate((a, b) => $"{a}, {b}") + ")";
    }
}
