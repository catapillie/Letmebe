using Letmebe.Lexing;

namespace Letmebe.Binding.Nodes {
    internal class BoundUnaryOperator {
        public enum Operation {
            Identity,

            IntegerNegation,
            FloatNegation,
            BooleanNegation,
            
            Unknown,
        }

        public static readonly BoundUnaryOperator[] UnaryOperators = new BoundUnaryOperator[] {
            // int
            new(TokenKind.PLUS,         Operation.Identity,         BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.MINUS,        Operation.IntegerNegation,  BoundPrimitiveType.IntegerPrimitive),

            // float
            new(TokenKind.PLUS,         Operation.Identity,         BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS,        Operation.FloatNegation,    BoundPrimitiveType.FloatPrimitive),

            // bool
            new(TokenKind.EXCLAMATION,  Operation.BooleanNegation,  BoundPrimitiveType.BooleanPrimitive),
        };

        public readonly Operation Op;
        public readonly TokenKind OperatorToken;
        public readonly BoundType OperandType;
        public readonly BoundType ReturnType;

        public BoundUnaryOperator(TokenKind operatorToken, Operation op, BoundType type)
            : this(operatorToken, op, type, type) { }

        public BoundUnaryOperator(TokenKind operatorToken, Operation op, BoundType operandType, BoundType returnType) {
            Op = op;
            OperatorToken = operatorToken;
            OperandType = operandType;
            ReturnType = returnType;
        }
    }
}
