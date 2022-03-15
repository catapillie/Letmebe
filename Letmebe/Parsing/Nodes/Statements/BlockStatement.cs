using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class BlockStatement : Statement {
        public readonly Token LeftBraceToken;
        public readonly Statement[] Statements;
        public readonly Token RightBraceToken;

        public BlockStatement(Token leftBraceToken, Statement[] statements, Token rightBraceToken) {
            LeftBraceToken = leftBraceToken;
            Statements = statements;
            RightBraceToken = rightBraceToken;
        }

        public override IEnumerable Children() {
            yield return LeftBraceToken;
            foreach (Statement statement in Statements)
                yield return statement;
            yield return RightBraceToken;
        }
    }
}
