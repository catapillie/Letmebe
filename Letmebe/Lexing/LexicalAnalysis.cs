using System.Globalization;
using System.Text.RegularExpressions;

namespace Letmebe.Lexing {
    internal static class LexicalAnalysis {
        public static (TokenKind? Kind, Regex Regex, Func<string, object> ValueFunc)[] Rules = {
            (null, new Regex(@"^---(.|\n)*---", RegexOptions.Compiled), null!),
            (null, new Regex(@"^#[^\n]*", RegexOptions.Compiled), null!),
            (null, new Regex(@"^\s+", RegexOptions.Compiled), null!),

            (TokenKind.LET, new Regex(@"^let(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.BE, new Regex(@"^be(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.DO, new Regex(@"^do(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.IF, new Regex(@"^if(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.UNLESS, new Regex(@"^unless(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.OTHERWISE, new Regex(@"^otherwise(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.WHILE, new Regex(@"^while(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.UNTIL, new Regex(@"^until(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.REPEAT, new Regex(@"^repeat(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.TIMES, new Regex(@"^times(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.FOREVER, new Regex(@"^forever(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.BREAK, new Regex(@"^break(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.RETURN, new Regex(@"^return(?!\w+)", RegexOptions.Compiled), null!),

            (TokenKind.TRUE, new Regex(@"^true(?!\w+)", RegexOptions.Compiled), s => true),
            (TokenKind.FALSE, new Regex(@"^false(?!\w+)", RegexOptions.Compiled), s => false),

            (TokenKind.INT, new Regex(@"^int(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.FLOAT, new Regex(@"^float(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.BOOL, new Regex(@"^bool(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.STR, new Regex(@"^str(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.CHAR, new Regex(@"^char(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.OBJ, new Regex(@"^obj(?!\w+)", RegexOptions.Compiled), null!),
            (TokenKind.TYPE, new Regex(@"^type(?!\w+)", RegexOptions.Compiled), null!),

            (TokenKind.DECIMAL, new Regex(@"^\d*\.?\d+f", RegexOptions.Compiled), s => float.Parse(s[0..^1], CultureInfo.InvariantCulture.NumberFormat)),
            (TokenKind.INTEGER, new Regex(@"^\d+", RegexOptions.Compiled), s => int.Parse(s)),
            (TokenKind.STRING, new Regex(@"^""[^""\n]*""", RegexOptions.Compiled), s => s[1..^1]),
            (TokenKind.CHARACTER, new Regex(@"^'[^\n]'", RegexOptions.Compiled), s => s[1]),
            (TokenKind.IDENTIFIER, new Regex(@"^\w+'*", RegexOptions.Compiled), null!),

            (TokenKind.RIGHTARROW, new Regex(@"^->", RegexOptions.Compiled), null!),
            (TokenKind.LEFTARROW, new Regex(@"^<-", RegexOptions.Compiled), null!),
            (TokenKind.DOTDOTDOT, new Regex(@"^\.{3}(?!\.)", RegexOptions.Compiled), null!),
            
            (TokenKind.EQUALS, new Regex(@"^==", RegexOptions.Compiled), null!),
            (TokenKind.NOTEQUALS, new Regex(@"^!=", RegexOptions.Compiled), null!),
            (TokenKind.MOREOREQUAL, new Regex(@"^>=", RegexOptions.Compiled), null!),
            (TokenKind.LESSOREQUAL, new Regex(@"^<=", RegexOptions.Compiled), null!),
            (TokenKind.EQUAL, new Regex(@"^=", RegexOptions.Compiled), null!),
            (TokenKind.PLUS, new Regex(@"^\+", RegexOptions.Compiled), null!),
            (TokenKind.MINUS, new Regex(@"^\-", RegexOptions.Compiled), null!),
            (TokenKind.ASTERISK, new Regex(@"^\*", RegexOptions.Compiled), null!),
            (TokenKind.SLASH, new Regex(@"^\/", RegexOptions.Compiled), null!),
            (TokenKind.CARET, new Regex(@"^\^", RegexOptions.Compiled), null!),
            (TokenKind.DOUBLEAMPERSAND, new Regex(@"^\&\&", RegexOptions.Compiled), null!),
            (TokenKind.DOUBLEBAR, new Regex(@"^\|\|", RegexOptions.Compiled), null!),
            (TokenKind.AMPERSAND, new Regex(@"^\&", RegexOptions.Compiled), null!),
            (TokenKind.BAR, new Regex(@"^\|", RegexOptions.Compiled), null!),
            (TokenKind.COLON, new Regex(@"^\:", RegexOptions.Compiled), null!),
            (TokenKind.SEMICOLON, new Regex(@"^\;", RegexOptions.Compiled), null!),
            (TokenKind.COMMA, new Regex(@"^\,", RegexOptions.Compiled), null!),
            (TokenKind.DOT, new Regex(@"^\.", RegexOptions.Compiled), null!),
            (TokenKind.EXCLAMATION, new Regex(@"^\!", RegexOptions.Compiled), null!),

            (TokenKind.LEFTPARENTHESIS, new Regex(@"^\(", RegexOptions.Compiled), null!),
            (TokenKind.RIGHTPARENTHESIS, new Regex(@"^\)", RegexOptions.Compiled), null!),
            (TokenKind.LEFTBRACKET, new Regex(@"^\[", RegexOptions.Compiled), null!),
            (TokenKind.RIGHTBRACKET, new Regex(@"^\]", RegexOptions.Compiled), null!),
            (TokenKind.LEFTBRACE, new Regex(@"^\{", RegexOptions.Compiled), null!),
            (TokenKind.RIGHTBRACE, new Regex(@"^\}", RegexOptions.Compiled), null!),
            (TokenKind.LEFTCHEVRON, new Regex(@"^\<", RegexOptions.Compiled), null!),
            (TokenKind.RIGHTCHEVRON, new Regex(@"^\>", RegexOptions.Compiled), null!),
        };
    }
}