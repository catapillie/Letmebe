using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class BinaryOperationExpression : Expression {
        public readonly Expression LeftOperand;
        public readonly Token Op;
        public readonly Expression RightOperand;

        public BinaryOperationExpression(Expression leftOperand, Token op, Expression rightOperand) {
            LeftOperand = leftOperand;
            Op = op;
            RightOperand = rightOperand;
        }

        public override IEnumerable Children() {
            yield return LeftOperand;
            yield return Op;
            yield return RightOperand;
        }
    }
}
