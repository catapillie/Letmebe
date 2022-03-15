using Letmebe.Lexing;

namespace Letmebe.Parsing {
    internal static class Rules {
        public static int UnaryOperationPrecedence(this TokenKind opKind)
            => opKind switch {
                TokenKind.PLUS or TokenKind.MINUS or TokenKind.EXCLAMATION => 4,
                _ => 0,
            };

        public static int BinaryOperationPrecedence(this TokenKind opKind)
            => opKind switch {
                TokenKind.CARET => 5,
                TokenKind.ASTERISK or TokenKind.SLASH => 4,
                TokenKind.PLUS or TokenKind.MINUS => 3,
                TokenKind.EQUALS or TokenKind.NOTEQUALS or TokenKind.MOREOREQUAL or TokenKind.LESSOREQUAL or TokenKind.RIGHTCHEVRON or TokenKind.LEFTCHEVRON => 2,
                TokenKind.AMPERSAND or TokenKind.BAR or TokenKind.DOUBLEAMPERSAND or TokenKind.DOUBLEBAR => 1,
                _ => 0,
            };

        public static bool IsPrimitiveTypeToken(this TokenKind token)
            => token is
                TokenKind.INT or
                TokenKind.FLOAT or
                TokenKind.BOOL or
                TokenKind.STR or 
                TokenKind.CHAR or 
                TokenKind.OBJ or 
                TokenKind.TYPE;

        public static bool IsBeginningOfExpression(this TokenKind token)
            => token.UnaryOperationPrecedence() > 0 || token is
                TokenKind.LEFTPARENTHESIS or
                TokenKind.INTEGER or
                TokenKind.DECIMAL or
                TokenKind.STRING or
                TokenKind.CHARACTER or
                TokenKind.TRUE or
                TokenKind.FALSE or
                TokenKind.IDENTIFIER;
    }
}
