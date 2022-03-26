namespace Letmebe.Binding.Nodes {
    internal sealed class BoundFunctionTemplate {
        public readonly BoundFunctionWord[] Words;

        public BoundFunctionTemplate(BoundFunctionWord[] words) {
            Words = words;
        }

        public bool Matches(BoundFunctionTemplate b) {
            if (Words.Length == b.Words.Length) {
                for (int i = 0; i < Words.Length; i++) {

                    if (Words[i] is BoundFunctionIdentifierWord identifierA && b.Words[i] is BoundFunctionIdentifierWord identifierB) {
                        if (identifierA.Identifier == identifierB.Identifier)
                            continue;
                        else return false;
                    }

                    if (Words[i] is BoundFunctionParameterWord paramA && b.Words[i] is BoundFunctionParameterWord paramB) {
                        if (paramA.Type.Is(paramB.Type))
                            continue;
                        else return false;
                    }

                    return false;
                }
                return true;
            }
            return false;
        }

        public override string ToString()
            => Words.Select(word => word.ToString()).Aggregate((a, b) => a + " " + b) ?? string.Empty;
    }
}
