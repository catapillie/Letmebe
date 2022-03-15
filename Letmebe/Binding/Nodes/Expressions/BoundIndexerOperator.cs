namespace Letmebe.Binding.Nodes {
    internal sealed class BoundIndexerOperator {
        public static readonly BoundIndexerOperator[] IndexerOperators = new BoundIndexerOperator[] {
            // string[int] -> char
            new(BoundPrimitiveType.StringPrimitive, new[] { BoundPrimitiveType.IntegerPrimitive }, BoundPrimitiveType.CharacterPrimitive),
        };

        public readonly BoundType IndexedType;
        public readonly BoundType[] IndexerTypes;
        public readonly BoundType ReturnType;

        public BoundIndexerOperator(BoundType indexedType, BoundType[] indexerTypes, BoundType returnType) {
            IndexedType = indexedType;
            IndexerTypes = indexerTypes;
            ReturnType = returnType;
        }
    }
}
