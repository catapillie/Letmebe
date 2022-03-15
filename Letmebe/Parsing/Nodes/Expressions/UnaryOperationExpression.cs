using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class UnaryOperationExpression : Expression {
        public readonly Token Op;
        public readonly Expression Operand;

        public UnaryOperationExpression(Token op, Expression operand) {
            Op = op;
            Operand = operand;
        }

        public override IEnumerable Children() {
            yield return Op;
            yield return Operand;
        }
    }
}
