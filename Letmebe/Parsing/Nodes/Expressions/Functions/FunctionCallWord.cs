namespace Letmebe.Parsing.Nodes {
    internal abstract class FunctionCallWord : SyntaxNode {
        public abstract Expression ToExpression();
    }
}
