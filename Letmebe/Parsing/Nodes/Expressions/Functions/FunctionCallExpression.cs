using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class FunctionCallExpression : Expression {
        public readonly FunctionCallWord[] Words;

        public FunctionCallExpression(FunctionCallWord[] words) {
            Words = words;
        }

        public override IEnumerable Children() {
            foreach (FunctionCallWord word in Words)
                yield return word;
        }
    }
}
