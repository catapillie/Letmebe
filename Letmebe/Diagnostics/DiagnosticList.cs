using System.Collections;

namespace Letmebe.Diagnostics {
    public class DiagnosticList : IEnumerable<Diagnostic> {
        private readonly List<Diagnostic> diagnostics = new List<Diagnostic>();

        internal void Add(Diagnostic diagnostic)
            => diagnostics.Add(diagnostic);

        public IEnumerator<Diagnostic> GetEnumerator() => diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
