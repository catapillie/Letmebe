using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class PrimaryTypeExpression : TypeExpression {
        public readonly Token TypeToken;

        public PrimaryTypeExpression(Token typeToken) {
            TypeToken = typeToken;
        }

        public override IEnumerable Children() {
            yield return TypeToken;
        }
    }
}
