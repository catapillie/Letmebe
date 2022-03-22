namespace Letmebe.Binding.Nodes {
    internal sealed class BoundTupleType : BoundType {
        public readonly BoundType[] Types;

        public BoundTupleType(params BoundType[] types) {
            Types = types;
        }

        public override bool Is(BoundType other, bool inherit) {
            if (other is BoundTupleType tuple) {
                if (Types.Length == tuple.Types.Length) {
                    for (int i = 0; i < Types.Length; i++)
                        if (!Types[i].Is(tuple.Types[i], inherit))
                            return inherit && base.Is(other, true);
                    return true;
                }
            }
            return inherit && base.Is(other, true);
        }

        public override string ToString()
            => "(" + Types.Select(t => t.ToString()).Aggregate((a, b) => $"{a}, {b}") + ")";
    }
}
