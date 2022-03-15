using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal abstract class SyntaxNode {
        public virtual IEnumerable Children() {
            yield break;
        }

        public SyntaxNode Clone()
            => (SyntaxNode)MemberwiseClone();
    }
}
