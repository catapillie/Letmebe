using Letmebe.Lexing;
using System.Collections;

namespace Letmebe.Parsing.Nodes {
    internal sealed class ProgramNode : SyntaxNode {
        public readonly Statement[] Statements;
        public readonly Token EOFToken;

        public ProgramNode(Statement[] statements, Token eofToken) {
            Statements = statements;
            EOFToken = eofToken;
        }

        public override IEnumerable Children() {
            foreach (Statement statement in Statements)
                yield return statement;
            yield return EOFToken;
        }
    }
}
