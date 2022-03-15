namespace Letmebe.Lexing {
    internal sealed class Token {
        public readonly TokenKind Kind;
        public readonly string Str;
        public readonly object Value;
        public readonly int Index;

        public Token(TokenKind kind, string str, object value, int index) {
            Kind = kind;
            Str = str;
            Value = value;
            Index = index;
        }

        public override string ToString() {
            return $"{Kind} {Value}";
        }
    }
}