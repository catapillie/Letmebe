using Letmebe.Lexing;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundBinaryOperator {
        public static readonly BoundBinaryOperator[] BinaryOperators = new BoundBinaryOperator[] {
            // int, int
            new(TokenKind.PLUS, BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.MINUS, BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.ASTERISK, BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.SLASH, BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.CARET, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            
            // float, float
            new(TokenKind.PLUS, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.ASTERISK, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.SLASH, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.CARET, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            
            // int, float
            new(TokenKind.PLUS, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.ASTERISK, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.SLASH, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.CARET, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            
            // float, int
            new(TokenKind.PLUS, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.ASTERISK, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.SLASH, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.CARET, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // bool, bool
            new(TokenKind.AMPERSAND, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.BAR, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.DOUBLEAMPERSAND, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.DOUBLEBAR, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.CARET, BoundPrimitiveType.BooleanPrimitive),

            // str, str
            new(TokenKind.PLUS, BoundPrimitiveType.StringPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // str, int
            new(TokenKind.ASTERISK, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.StringPrimitive),

            // char, char
            new(TokenKind.PLUS, BoundPrimitiveType.CharacterPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // char, int
            new(TokenKind.ASTERISK, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.StringPrimitive),

            // char, str
            new(TokenKind.PLUS, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.StringPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // str, char
            new(TokenKind.PLUS, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive),
            new(TokenKind.EQUALS, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // obj, obj
            new(TokenKind.EQUALS, BoundPrimitiveType.ObjectPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.ObjectPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // type, type
            new(TokenKind.EQUALS, BoundPrimitiveType.TypePrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS, BoundPrimitiveType.TypePrimitive, BoundPrimitiveType.BooleanPrimitive),
        };

        public readonly TokenKind OperatorToken;
        public readonly BoundType LeftType;
        public readonly BoundType ReturnType;
        public readonly BoundType RightType;

        public BoundBinaryOperator(TokenKind operatorToken, BoundType type)
            : this(operatorToken, type, type, type) { }

        public BoundBinaryOperator(TokenKind operatorToken, BoundType operandType, BoundType returnType)
            : this(operatorToken, operandType, operandType, returnType) { }

        public BoundBinaryOperator(TokenKind operatorToken, BoundType leftType, BoundType rightType, BoundType returnType) {
            OperatorToken = operatorToken;
            LeftType = leftType;
            ReturnType = returnType;
            RightType = rightType;
        }
    }
}
