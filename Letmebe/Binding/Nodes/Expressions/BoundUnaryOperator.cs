using Letmebe.Lexing;

namespace Letmebe.Binding.Nodes {
    internal class BoundUnaryOperator {
        public static readonly BoundUnaryOperator[] UnaryOperators = new BoundUnaryOperator[] {
            // int
            new(TokenKind.PLUS, BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.MINUS, BoundPrimitiveType.IntegerPrimitive),

            // float
            new(TokenKind.PLUS, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS, BoundPrimitiveType.FloatPrimitive),

            // bool
            new(TokenKind.EXCLAMATION, BoundPrimitiveType.BooleanPrimitive),
        };

        public readonly TokenKind OperatorToken;
        public readonly BoundType OperandType;
        public readonly BoundType ReturnType;

        public BoundUnaryOperator(TokenKind operatorToken, BoundType type)
            : this(operatorToken, type, type) { }

        public BoundUnaryOperator(TokenKind operatorToken, BoundType operandType, BoundType returnType) {
            OperatorToken = operatorToken;
            OperandType = operandType;
            ReturnType = returnType;
        }
    }
}
