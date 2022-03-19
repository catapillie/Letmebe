namespace Letmebe.Binding.Nodes {
    internal sealed class BoundIndexerOperator {
        public enum Operation {
            StringIndexing,

            Unknown,
        }

        public static readonly BoundIndexerOperator[] IndexerOperators = new BoundIndexerOperator[] {
            // string[int] -> char
            new(BoundPrimitiveType.StringPrimitive, Operation.StringIndexing, new[] { BoundPrimitiveType.IntegerPrimitive }, BoundPrimitiveType.CharacterPrimitive),
        };

        public readonly Operation Op;
        public readonly BoundType IndexedType;
        public readonly BoundType[] IndexerTypes;
        public readonly BoundType ReturnType;

        public BoundIndexerOperator(BoundType indexedType, Operation op, BoundType[] indexerTypes, BoundType returnType) {
            Op = op;
            IndexedType = indexedType;
            IndexerTypes = indexerTypes;
            ReturnType = returnType;
        }
    }
}
