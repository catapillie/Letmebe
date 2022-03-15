using System.Collections;

namespace Letmebe.Binding.Nodes {
    internal abstract class BoundNode {
        public virtual IEnumerable Children() {
            yield break;
        }
    }
}
