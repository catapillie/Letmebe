namespace Letmebe.Lexing {
    internal enum TokenKind {
        EOF,
        UNKNOWN,

        LET, BE, DO,
        IF, UNLESS, OTHERWISE,
        WHILE, UNTIL,
        REPEAT, TIMES,
        FOREVER,
        BREAK,
        RETURN,

        TRUE,
        FALSE,

        INT, FLOAT, BOOL,
        STR, CHAR,
        OBJ, TYPE,

        DECIMAL,
        INTEGER,
        STRING,
        CHARACTER,
        IDENTIFIER,

        RIGHTARROW, LEFTARROW,
        DOTDOTDOT,

        EQUALS, NOTEQUALS, MOREOREQUAL, LESSOREQUAL,
        EQUAL,
        PLUS, MINUS, ASTERISK, SLASH, CARET,
        DOUBLEAMPERSAND, DOUBLEBAR,
        AMPERSAND, BAR,
        COLON, SEMICOLON, COMMA, DOT, EXCLAMATION,

        LEFTPARENTHESIS, RIGHTPARENTHESIS,
        LEFTBRACKET, RIGHTBRACKET,
        LEFTBRACE, RIGHTBRACE,
        LEFTCHEVRON, RIGHTCHEVRON,
    }
}