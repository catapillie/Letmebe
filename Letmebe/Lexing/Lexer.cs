using Letmebe.Diagnostics;
using System.Text.RegularExpressions;

namespace Letmebe.Lexing {
    internal sealed class Lexer {
        private readonly string source;
        private int index;

        public readonly DiagnosticList Diagnostics;

        public Lexer(string source, DiagnosticList diagnostics) {
            this.source = source;
            Diagnostics = diagnostics;
        }

        public Token Lex() {
            if (index >= source.Length)
                return new Token(TokenKind.EOF, "\0", null!, index);
            else {
                string remaining = source[index..];

                foreach (var rule in LexicalAnalysis.Rules) {
                    Match match = rule.Regex.Match(remaining);
                    if (match.Success) {
                        index += match.Length;

                        if (!rule.Kind.HasValue)
                            return Lex();

                        return new Token(rule.Kind.Value, match.Value, rule.ValueFunc?.Invoke(match.Value) ?? match.Value, match.Index);
                    }
                }

                char c = remaining[0];
                Diagnostics.Add(Reports.UnexpectedCharacter(c));
                return new Token(TokenKind.UNKNOWN, c.ToString(), null!, index++);
            }
        }
    }
}