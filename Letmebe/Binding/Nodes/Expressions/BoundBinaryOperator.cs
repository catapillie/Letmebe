using Letmebe.Lexing;

namespace Letmebe.Binding.Nodes {
    internal sealed class BoundBinaryOperator {
        public enum Operation {
            IntegerAddition,
            IntegerSubtraction,
            IntegerMultiplication,
            IntegerDivision,
            IntegerExponentiation,
            IntegerEquality,
            IntegerInequality,
            IntegerLessThan,
            IntegerGreaterThan,
            IntegerLessThanOrEqual,
            IntegerGreaterThanOrEqual,

            FloatAddition,
            FloatSubtraction,
            FloatMultiplication,
            FloatDivision,
            FloatExponentiation,
            FloatEquality,
            FloatInequality,
            FloatLessThan,
            FloatGreaterThan,
            FloatLessThanOrEqual,
            FloatGreaterThanOrEqual,

            IntegerFloatAddition,
            IntegerFloatSubtraction,
            IntegerFloatMultiplication,
            IntegerFloatDivision,
            IntegerFloatExponentiation,
            IntegerFloatEquality,
            IntegerFloatInequality,
            IntegerFloatLessThan,
            IntegerFloatGreaterThan,
            IntegerFloatLessThanOrEqual,
            IntegerFloatGreaterThanOrEqual,

            FloatIntegerAddition,
            FloatIntegerSubtraction,
            FloatIntegerMultiplication,
            FloatIntegerDivision,
            FloatIntegerExponentiation,
            FloatIntegerEquality,
            FloatIntegerInequality,
            FloatIntegerLessThan,
            FloatIntegerGreaterThan,
            FloatIntegerLessThanOrEqual,
            FloatIntegerGreaterThanOrEqual,

            LogicalAnd,
            LogicalOr,
            LogicalLazyAnd,
            LogicalLazyOr,
            LogicalXor,

            StringConcatenation,
            StringEquality,
            StringInequality,
            StringLessThan,
            StringGreaterThan,
            StringLessThanOrEqual,
            StringGreaterThanOrEqual,

            StringRepetition,

            CharacterConcatenation,
            CharacterEquality,
            CharacterInequality,
            CharacterLessThan,
            CharacterGreaterThan,
            CharacterLessThanOrEqual,
            CharacterGreaterThanOrEqual,

            CharacterRepetition,

            CharacterStringConcatenation,
            CharacterStringEquality,
            CharacterStringInequality,
            CharacterStringLessThan,
            CharacterStringGreaterThan,
            CharacterStringLessThanOrEqual,
            CharacterStringGreaterThanOrEqual,

            StringCharacterConcatenation,
            StringCharacterEquality,
            StringCharacterInequality,
            StringCharacterLessThan,
            StringCharacterGreaterThan,
            StringCharacterLessThanOrEqual,
            StringCharacterGreaterThanOrEqual,

            ObjectEquality,
            ObjectInequality,

            TypeEquality,
            TypeInequality,

            Unknown,
        }

        public static readonly BoundBinaryOperator[] BinaryOperators = new BoundBinaryOperator[] {
            // int, int
            new(TokenKind.PLUS,             Operation.IntegerAddition,                      BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.MINUS,            Operation.IntegerSubtraction,                   BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.ASTERISK,         Operation.IntegerMultiplication,                BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.SLASH,            Operation.IntegerDivision,                      BoundPrimitiveType.IntegerPrimitive),
            new(TokenKind.CARET,            Operation.IntegerExponentiation,                BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS,           Operation.IntegerEquality,                      BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.IntegerInequality,                    BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.IntegerLessThan,                      BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.IntegerGreaterThan,                   BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.IntegerLessThanOrEqual,               BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.IntegerGreaterThanOrEqual,            BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            
            // float, float
            new(TokenKind.PLUS,             Operation.FloatAddition,                        BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS,            Operation.FloatSubtraction,                     BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.ASTERISK,         Operation.FloatMultiplication,                  BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.SLASH,            Operation.FloatDivision,                        BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.CARET,            Operation.FloatExponentiation,                  BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS,           Operation.FloatEquality,                        BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.FloatInequality,                      BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.FloatLessThan,                        BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.FloatGreaterThan,                     BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.FloatLessThanOrEqual,                 BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.FloatGreaterThanOrEqual,              BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            
            // int, float
            new(TokenKind.PLUS,             Operation.IntegerFloatAddition,                 BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS,            Operation.IntegerFloatSubtraction,              BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.ASTERISK,         Operation.IntegerFloatMultiplication,           BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.SLASH,            Operation.IntegerFloatDivision,                 BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.CARET,            Operation.IntegerFloatExponentiation,           BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS,           Operation.IntegerFloatEquality,                 BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.IntegerFloatInequality,               BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.IntegerFloatLessThan,                 BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.IntegerFloatGreaterThan,              BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.IntegerFloatLessThanOrEqual,          BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.IntegerFloatGreaterThanOrEqual,       BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.BooleanPrimitive),
            
            // float, int
            new(TokenKind.PLUS,             Operation.FloatIntegerAddition,                 BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.MINUS,            Operation.FloatIntegerSubtraction,              BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.ASTERISK,         Operation.FloatIntegerMultiplication,           BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.SLASH,            Operation.FloatIntegerDivision,                 BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.CARET,            Operation.FloatIntegerExponentiation,           BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.FloatPrimitive),
            new(TokenKind.EQUALS,           Operation.FloatIntegerEquality,                 BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.FloatIntegerInequality,               BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.FloatIntegerLessThan,                 BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.FloatIntegerGreaterThan,              BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.FloatIntegerLessThanOrEqual,          BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.FloatIntegerGreaterThanOrEqual,       BoundPrimitiveType.FloatPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // bool, bool
            new(TokenKind.AMPERSAND,        Operation.LogicalAnd,                           BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.BAR,              Operation.LogicalOr,                            BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.DOUBLEAMPERSAND,  Operation.LogicalLazyAnd,                       BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.DOUBLEBAR,        Operation.LogicalLazyOr,                        BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.CARET,            Operation.LogicalXor,                           BoundPrimitiveType.BooleanPrimitive),

            // str, str
            new(TokenKind.PLUS,             Operation.StringConcatenation,                  BoundPrimitiveType.StringPrimitive),
            new(TokenKind.EQUALS,           Operation.StringEquality,                       BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.StringInequality,                     BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.StringLessThan,                       BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.StringGreaterThan,                    BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.StringLessThanOrEqual,                BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.StringGreaterThanOrEqual,             BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // str, int
            new(TokenKind.ASTERISK,         Operation.StringRepetition,                     BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.StringPrimitive),

            // char, char
            new(TokenKind.PLUS,             Operation.CharacterConcatenation,               BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive),
            new(TokenKind.EQUALS,           Operation.CharacterEquality,                    BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.CharacterInequality,                  BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.CharacterLessThan,                    BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.CharacterGreaterThan,                 BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.CharacterLessThanOrEqual,             BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.CharacterGreaterThanOrEqual,          BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // char, int
            new(TokenKind.ASTERISK,         Operation.CharacterRepetition,                  BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.IntegerPrimitive, BoundPrimitiveType.StringPrimitive),

            // char, str
            new(TokenKind.PLUS,             Operation.CharacterStringConcatenation,         BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.StringPrimitive),
            new(TokenKind.EQUALS,           Operation.CharacterStringEquality,              BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.CharacterStringInequality,            BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.CharacterStringLessThan,              BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.CharacterStringGreaterThan,           BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.CharacterStringLessThanOrEqual,       BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.CharacterStringGreaterThanOrEqual,    BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // str, char
            new(TokenKind.PLUS,             Operation.StringCharacterConcatenation,         BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.StringPrimitive),
            new(TokenKind.EQUALS,           Operation.StringCharacterEquality,              BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.StringCharacterInequality,            BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LEFTCHEVRON,      Operation.StringCharacterLessThan,              BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.RIGHTCHEVRON,     Operation.StringCharacterGreaterThan,           BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.LESSOREQUAL,      Operation.StringCharacterLessThanOrEqual,       BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.MOREOREQUAL,      Operation.StringCharacterGreaterThanOrEqual,    BoundPrimitiveType.StringPrimitive, BoundPrimitiveType.CharacterPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // obj, obj
            new(TokenKind.EQUALS,           Operation.ObjectEquality,                       BoundPrimitiveType.ObjectPrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.ObjectInequality,                     BoundPrimitiveType.ObjectPrimitive, BoundPrimitiveType.BooleanPrimitive),

            // type, type
            new(TokenKind.EQUALS,           Operation.TypeEquality,                         BoundPrimitiveType.TypePrimitive, BoundPrimitiveType.BooleanPrimitive),
            new(TokenKind.NOTEQUALS,        Operation.TypeInequality,                       BoundPrimitiveType.TypePrimitive, BoundPrimitiveType.BooleanPrimitive),
        };

        public readonly TokenKind OperatorToken;
        public readonly Operation Op;
        public readonly BoundType LeftType;
        public readonly BoundType ReturnType;
        public readonly BoundType RightType;

        public BoundBinaryOperator(TokenKind operatorToken, Operation op, BoundType type)
            : this(operatorToken, op, type, type, type) { }

        public BoundBinaryOperator(TokenKind operatorToken, Operation op, BoundType operandType, BoundType returnType)
            : this(operatorToken, op, operandType, operandType, returnType) { }

        public BoundBinaryOperator(TokenKind operatorToken, Operation op, BoundType leftType, BoundType rightType, BoundType returnType) {
            Op = op;
            OperatorToken = operatorToken;
            LeftType = leftType;
            ReturnType = returnType;
            RightType = rightType;
        }
    }
}
