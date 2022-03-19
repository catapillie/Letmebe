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

        internal static Diagnostic ExpressionStatementMustBeFunctionCall() {
            return new("An expression can only be a statement if it is a function call", DiagnosticCode.LMB0023);
        }

        internal static Diagnostic VariableAlreadyExists(BoundSymbol symbol) {
            return new($"A variable named '{symbol.Name}' already exists in current scope", DiagnosticCode.LMB0024);
        }

        internal static Diagnostic RepeatTimesAmountMustBeInteger() {
            return new("Repeat-times amount expression must be an integer", DiagnosticCode.LMB0025);
        }

        internal static Diagnostic DoWhileConditionMustBeBoolean() {
            return new("Do-while condition expression must be a boolean", DiagnosticCode.LMB0026);
        }

        internal static Diagnostic DoUntilConditionMustBeBoolean() {
            return new("Do-until condition expression must be a boolean", DiagnosticCode.LMB0027);
        }

        internal static Diagnostic IfConditionMustBeBoolean() {
            return new("If condition expression must be a boolean", DiagnosticCode.LMB0028);
        }

        internal static Diagnostic UnlessConditionMustBeBoolean() {
            return new("Unless condition expression must be a boolean", DiagnosticCode.LMB0029);
        }

        internal static Diagnostic IfOtherwiseConditionMustBeBoolean() {
            return new("If-otherwise condition expression must be a boolean", DiagnosticCode.LMB0030);
        }

        internal static Diagnostic WhileConditionMustBeBoolean() {
            return new("While condition expression must be a boolean", DiagnosticCode.LMB0031);
        }

        internal static Diagnostic UntilConditionMustBeBoolean() {
            return new("Until condition expression must be a boolean", DiagnosticCode.LMB0032);
        }

        internal static Diagnostic FunctionSignatureMustHaveIdentifier() {
            return new("A newly defined function's signature must have at least one identifier", DiagnosticCode.LMB0033);
        }

        internal static Diagnostic FunctionParameterAlreadyDeclared(BoundSymbol symbol) {
            return new($"A parameter named '{symbol.Name}' ({symbol.Type}) was already declared in this function definition", DiagnosticCode.LMB0034);
        }

        internal static Diagnostic FunctionAlreadyDefined(BoundFunctionSymbol functionSymbol) {
            return new($"A function with signature '{functionSymbol}' already exists in current scope", DiagnosticCode.LMB0035);
        }
    }
}
