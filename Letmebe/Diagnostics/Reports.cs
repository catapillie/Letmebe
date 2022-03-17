using Letmebe.Binding.Nodes;
using Letmebe.Lexing;

namespace Letmebe.Diagnostics {
    internal static class Reports {
        public static Diagnostic UnexpectedCharacter(char c) {
            return new($"Unexpected character '{c}'", DiagnosticCode.LMB0001);
        }

        internal static Diagnostic UnexpectedToken(TokenKind found, TokenKind expected) {
            return new($"Unexpected token {found}, expected {expected}", DiagnosticCode.LMB0002);
        }

        internal static Diagnostic ExpectedExpression() {
            return new("Expected an expression", DiagnosticCode.LMB0003);
        }

        internal static Diagnostic ExpectedStatementAfterIf() {
            return new("An if statement must be followed by another statement", DiagnosticCode.LMB0004);
        }

        internal static Diagnostic ExpectedStatementAfterIfOtherwise() {
            return new("An if-otherwise statement must be followed by another statement", DiagnosticCode.LMB0005);
        }

        internal static Diagnostic ExpectedStatementAfterUnless() {
            return new("An unless statement must be followed by another statement", DiagnosticCode.LMB0006);
        }

        internal static Diagnostic ExpectedStatementAfterForever() {
            return new("A forever statement must be followed by another statement", DiagnosticCode.LMB0007);
        }

        internal static Diagnostic ExpectedStatementAfterRepeatTimes() {
            return new("A repeat-times statement must be followed by another statement", DiagnosticCode.LMB0008);
        }

        internal static Diagnostic ExpectedStatementAfterWhile() {
            return new("A while statement must be followed by another statement", DiagnosticCode.LMB0009);
        }

        internal static Diagnostic ExpectedStatementAfterUntil() {
            return new("An until statement must be followed by another statement", DiagnosticCode.LMB0010);
        }

        internal static Diagnostic ExpectedStatementAfterDo() {
            return new("A do statement must be followed by another statement", DiagnosticCode.LMB0011);
        }

        internal static Diagnostic ExpectedWhileOrUntilAfterDoStatement() {
            return new("A do statement followed by a non expression statement must be followed by either 'while' or 'until'", DiagnosticCode.LMB0012);
        }

        internal static Diagnostic ExpectedStatementAfterFunctionDefinition() {
            return new("A function definition must be followed by another statement", DiagnosticCode.LMB0013);
        }

        internal static Diagnostic CannotUseBlockStatement() {
            return new("Block statements cannot be used unaccompanied by a control flow statement", DiagnosticCode.LMB0014);
        }

        internal static Diagnostic ExpectedTypeExpression() {
            return new("Expected a type expression", DiagnosticCode.LMB0015);
        }

        internal static Diagnostic ExpectedSentence() {
            return new("Expected at least one identifier or parameter", DiagnosticCode.LMB0016);
        }

        internal static Diagnostic UndefinedTypeInCurrentScope(string name, int genericArgCount) {
            if (genericArgCount == 0)
                return new($"Type '{name}' is undefined in the current scope", DiagnosticCode.LMB0017);
            else if (genericArgCount == 1)
                return new($"Generic type '{name}' with a type argument is undefined in the current scope", DiagnosticCode.LMB0018);
            else
                return new($"Generic type '{name}' with {genericArgCount} type arguments is undefined in the current scope", DiagnosticCode.LMB0018);
        }

        internal static Diagnostic UndefinedVariableInScope(string name) {
            return new($"Variable '{name}' does not exist in current scope", DiagnosticCode.LMB0019);
        }

        internal static Diagnostic UndefinedBinaryOperator(TokenKind op, BoundType leftType, BoundType rightType) {
            return new($"Binary operator '{op}' does not exist between types {leftType} and {rightType}", DiagnosticCode.LMB0020);
        }

        internal static Diagnostic UndefinedUnaryOperator(TokenKind op, BoundType leftType) {
            return new($"Unary operator '{op}' does not exist on type {leftType}", DiagnosticCode.LMB0021);
        }

        internal static Diagnostic UndefinedIndexerOperator(BoundType indexedType, BoundType indexerType) {
            return new($"Indexer operator {indexedType}[{indexerType}] does not exist", DiagnosticCode.LMB0022);
        }

        internal static Diagnostic UndefinedFunction(BoundFunctionTemplate template) {
            return new($"Function with signature '{template}' does not exist in current scope", DiagnosticCode.LMB0023);
        }
    }
}
