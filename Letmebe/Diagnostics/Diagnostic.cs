namespace Letmebe.Diagnostics {
    public sealed record Diagnostic(string Message, DiagnosticCode Code) {
        public override string ToString() => $"{Code}: {Message}";
    }
}
