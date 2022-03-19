using Letmebe.Diagnostics;
using Letmebe.Lexing;
using Letmebe.Parsing.Nodes;

namespace Letmebe.Parsing {
    internal sealed class Parser {
        private readonly Lexer lexer;
        private Token lookahead;

        public readonly DiagnosticList Diagnostics = new();

        public Parser(string source) {
            lexer = new Lexer(source, Diagnostics);
            lookahead = lexer.Lex();
        }

        private Token Step() {
            Token token = lookahead;
            if (lookahead.Kind != TokenKind.EOF)
                lookahead = lexer.Lex();
            return token;
        }

        private bool WillMatch(TokenKind kind)
            => lookahead.Kind == kind;

        private Token Match(TokenKind kind) {
            if (lookahead.Kind == kind)
                return Step();

            Diagnostics.Add(Reports.UnexpectedToken(lookahead.Kind, kind));
            return new Token(kind, null!, null!, lookahead.Index);
        }

        private bool TryMatch(TokenKind kind, out Token node) {
            if (lookahead.Kind == kind) {
                node = Step();
                return true;
            }

            node = new Token(kind, string.Empty, null!, lookahead.Index);
            return false;
        }

        public ProgramNode Parse()
            => ParseProgram();

        private ProgramNode ParseProgram() {
            List<Statement> statements = new();
            while (true) {
                var statement = ParseStatement(out bool fail, allowBlocks: false);
                if (fail)
                    break;
                statements.Add(statement);
            }
            return new ProgramNode(statements.ToArray(), Match(TokenKind.EOF));
        }

        private TypeExpression ParseTypeExpression() {
            TypeExpression? typeExpr;

            if (WillMatch(TokenKind.LEFTPARENTHESIS))
                typeExpr = ParseParenthesizedOrTupleTypeExpression();
            else if (WillMatch(TokenKind.LEFTBRACKET))
                typeExpr = ParseArrayTypeExpression();
            else if (lookahead.Kind.IsPrimitiveTypeToken())
                typeExpr = new PrimaryTypeExpression(Step());
            else if (WillMatch(TokenKind.IDENTIFIER)) {
                typeExpr = ParseIdentifierTypeExpression();
            } else {
                Diagnostics.Add(Reports.ExpectedTypeExpression());
                typeExpr = new();
            }

            if (TryMatch(TokenKind.ARROW, out var arrowToken)) {
                var returnTypeExpression = ParseTypeExpression();
                return new FunctionTypeExpression(typeExpr, arrowToken, returnTypeExpression);
            }

            return typeExpr;
        }

        private TypeExpression ParseParenthesizedOrTupleTypeExpression() {
            var leftParenToken = Step();
            List<TypeExpression> typeExpressions = new();
            do {
                typeExpressions.Add(ParseTypeExpression());
            } while (TryMatch(TokenKind.COMMA, out _));
            var rightParenToken = Match(TokenKind.RIGHTPARENTHESIS);

            if (typeExpressions.Count > 1)
                return new TupleTypeExpression(leftParenToken, typeExpressions.ToArray(), rightParenToken);
            else
                return new ParenthesizedTypeExpression(leftParenToken, typeExpressions[0], rightParenToken);
        }

        private ArrayTypeExpression ParseArrayTypeExpression() {
            var leftBracketToken = Step();
            var typeExpression = ParseTypeExpression();
            var rightBracketToken = Match(TokenKind.RIGHTBRACKET);
            return new ArrayTypeExpression(leftBracketToken, typeExpression, rightBracketToken);
        }

        private TypeExpression ParseIdentifierTypeExpression() {
            var identifier = Step();

            if (TryMatch(TokenKind.LEFTCHEVRON, out var leftChevronToken)) {
                List<TypeExpression> typeExpressions = new();
                do {
                    typeExpressions.Add(ParseTypeExpression());
                } while (TryMatch(TokenKind.COMMA, out _));
                var rightChevronToken = Match(TokenKind.RIGHTCHEVRON);
                return new GenericTypeExpression(identifier, leftChevronToken, typeExpressions.ToArray(), rightChevronToken);
            }

            return new PrimaryTypeExpression(identifier);
        }

        private Statement ParseStatement(out bool failure, bool allowBlocks = true, bool parseExpressions = true) {
            failure = false;

            if (TryMatch(TokenKind.DOTDOTDOT, out var emptyToken))
                return new EmptyStatement(emptyToken);

            if (WillMatch(TokenKind.LEFTBRACE)) {
                if (!allowBlocks)
                    Diagnostics.Add(Reports.CannotUseBlockStatement());
                return ParseBlockStatement();
            }

            if (WillMatch(TokenKind.IF))
                return ParseIfStatement();

            if (WillMatch(TokenKind.UNLESS))
                return ParseUnlessStatement();

            if (WillMatch(TokenKind.WHILE))
                return ParseWhileStatement();

            if (WillMatch(TokenKind.UNTIL))
                return ParseUntilStatement();

            if (WillMatch(TokenKind.DO))
                return ParseDoStatement();

            if (WillMatch(TokenKind.FOREVER))
                return ParseForeverStatement();

            if (WillMatch(TokenKind.REPEAT))
                return ParseRepeatTimesStatement();

            if (WillMatch(TokenKind.LET))
                return ParseLetStatement();

            if (WillMatch(TokenKind.RETURN))
                return ParseReturnStatement();

            if (parseExpressions && lookahead.Kind.IsBeginningOfExpression())
                return new ExpressionStatement(ParseExpression(), Match(TokenKind.SEMICOLON));

            failure = true;
            return new Statement();
        }

        private BlockStatement ParseBlockStatement() {
            var leftBraceToken = Step();
            List<Statement> statements = new();
            while (true) {
                var statement = ParseStatement(out bool fail, allowBlocks: false);
                if (fail)
                    break;
                statements.Add(statement);
            }
            var rightBraceToken = Match(TokenKind.RIGHTBRACE);
            return new BlockStatement(leftBraceToken, statements.ToArray(), rightBraceToken);
        }

        private IfStatement ParseIfStatement() {
            var ifToken = Step();
            var condition = ParseExpression();
            var then = ParseStatement(out bool fail);
            if (fail) {
                Diagnostics.Add(Reports.ExpectedStatementAfterIf());
                then = new Statement();
            }
            if (TryMatch(TokenKind.OTHERWISE, out var otherwiseToken)) {
                var otherwise = ParseStatement(out fail);
                if (fail) {
                    Diagnostics.Add(Reports.ExpectedStatementAfterIfOtherwise());
                    otherwise = new Statement();
                }
                return new IfStatement(ifToken, condition, then, otherwiseToken, otherwise);
            }
            return new IfStatement(ifToken, condition, then);
        }

        private UnlessStatement ParseUnlessStatement() {
            var unlessToken = Step();
            var condition = ParseExpression();
            var then = ParseStatement(out bool fail);
            if (fail) {
                Diagnostics.Add(Reports.ExpectedStatementAfterUnless());
                then = new Statement();
            }
            return new UnlessStatement(unlessToken, condition, then);
        }

        private WhileStatement ParseWhileStatement() {
            var whileToken = Step();
            var condition = ParseExpression();
            var then = ParseStatement(out bool fail);
            if (fail) {
                Diagnostics.Add(Reports.ExpectedStatementAfterWhile());
                then = new Statement();
            }
            return new WhileStatement(whileToken, condition, then);
        }

        private UntilStatement ParseUntilStatement() {
            var untilToken = Step();
            var condition = ParseExpression();
            var then = ParseStatement(out bool fail);
            if (fail) {
                Diagnostics.Add(Reports.ExpectedStatementAfterUntil());
                then = new Statement();
            }
            return new UntilStatement(untilToken, condition, then);
        }

        private Statement ParseDoStatement() {
            var doToken = Step();
            var statement = ParseStatement(out bool fail);
            if (fail) {
                Diagnostics.Add(Reports.ExpectedStatementAfterDo());
                statement = new Statement();
            }

            if (TryMatch(TokenKind.WHILE, out var whileToken)) {
                var condition = ParseExpression();
                var semicolonToken = Match(TokenKind.SEMICOLON);
                return new DoWhileStatement(doToken, statement, whileToken, condition, semicolonToken);
            }
            if (TryMatch(TokenKind.UNTIL, out var untilToken)) {
                var condition = ParseExpression();
                var semicolonToken = Match(TokenKind.SEMICOLON);
                return new DoUntilStatement(doToken, statement, untilToken, condition, semicolonToken);
            }

            if (statement is not ExpressionStatement)
                Diagnostics.Add(Reports.ExpectedWhileOrUntilAfterDoStatement());
            return new DoStatement(doToken, statement);
        }

        private ForeverStatement ParseForeverStatement() {
            var foreverToken = Step();
            var statement = ParseStatement(out bool fail);
            if (fail) {
                Diagnostics.Add(Reports.ExpectedStatementAfterForever());
                statement = new Statement();
            }
            return new ForeverStatement(foreverToken, statement);
        }

        private RepeatTimesStatement ParseRepeatTimesStatement() {
            var repeatToken = Step();
            var amount = ParseExpression();
            var timesToken = Match(TokenKind.TIMES);
            var statement = ParseStatement(out bool fail);
            if (fail) {
                Diagnostics.Add(Reports.ExpectedStatementAfterRepeatTimes());
                statement = new Statement();
            }
            return new RepeatTimesStatement(repeatToken, amount, timesToken, statement);
        }

        private Statement ParseLetStatement() {
            var letToken = Step();
            var sentence = ParseSentence();

            if (TryMatch(TokenKind.DO, out var doToken)) {
                var body = ParseStatement(out bool fail);
                if (fail) {
                    Diagnostics.Add(Reports.ExpectedStatementAfterFunctionDefinition());
                    body = new Statement();
                }
                return new VoidFunctionDefinitionStatement(letToken, sentence, doToken, body);
            }

            var beToken = Match(TokenKind.BE);
            var typeExpression = ParseTypeExpression();

            if (sentence.IsIdentifier) {
                Expression expression = null!;

                var body = ParseStatement(out bool fail, parseExpressions: false);
                if (!fail)
                    return new FunctionDefinitionStatement(letToken, sentence, beToken, typeExpression, body);

                expression ??= ParseExpression();
                var semicolonToken = Match(TokenKind.SEMICOLON);
                return new VariableDeclarationStatement(letToken, (Token)sentence, beToken, typeExpression, expression, semicolonToken);
            } else {
                var body = ParseStatement(out bool fail);
                if (fail) {
                    Diagnostics.Add(Reports.ExpectedStatementAfterFunctionDefinition());
                    body = new Statement();
                }
                return new FunctionDefinitionStatement(letToken, sentence, beToken, typeExpression, body);
            }
        }

        private ReturnStatement ParseReturnStatement() {
            var returnToken = Step();

            if (TryMatch(TokenKind.SEMICOLON, out var semicolonToken))
                return new ReturnStatement(returnToken, semicolonToken);

            var expression = ParseExpression();
            semicolonToken = Match(TokenKind.SEMICOLON);
            return new ReturnStatement(returnToken, expression, semicolonToken);
        }

        private DefinitionSentence ParseSentence() {
            List<DefinitionWord> words = new();
            while (true) {
                DefinitionWord? word = null;

                if (TryMatch(TokenKind.IDENTIFIER, out var identifierToken))
                    word = new DefinitionIdentifierWord(identifierToken);

                if (TryMatch(TokenKind.LEFTCHEVRON, out var leftChevronToken)) {
                    List<(TypeExpression, Token)> parameters = new();
                    do {
                        var typeExpression = ParseTypeExpression();
                        var identifier = Match(TokenKind.IDENTIFIER);
                        parameters.Add((typeExpression, identifier));
                    } while (TryMatch(TokenKind.COMMA, out _));
                    var rightChevronToken = Match(TokenKind.RIGHTCHEVRON);
                    word = new DefinitionParameterListWord(leftChevronToken, parameters.ToArray(), rightChevronToken);
                }

                if (word is null) break;
                words.Add(word);
            }

            if (words.Count == 0)
                Diagnostics.Add(Reports.ExpectedSentence());
            return new DefinitionSentence(words.ToArray());
        }

        private Expression ParseExpression() {
            List<FunctionCallWord> words = new();
            do {
                var expression = ParseOperationExpression(0);
                if (expression is VariableLiteral variable)
                    words.Add(new FunctionCallIdentifier(variable));
                else
                    words.Add(new FunctionCallParameter(expression));
            } while (lookahead.Kind.IsBeginningOfExpression());

            if (words.Count > 1)
                return new FunctionCallExpression(words.ToArray());
            else
                return words[0].ToExpression();
        }

        private Expression ParseOperationExpression(int precedence) {
            Expression left;
            
            int unaryPrecedence = lookahead.Kind.UnaryOperationPrecedence();
            if (unaryPrecedence == 0 || unaryPrecedence < precedence)
                left = ParsePrimaryExpression();
            else {
                var op = Step();
                var operand = ParseOperationExpression(unaryPrecedence);
                left = new UnaryOperationExpression(op, operand);
            }

            while (true) {
                int binaryPrecedence = lookahead.Kind.BinaryOperationPrecedence();
                if (binaryPrecedence == 0 || binaryPrecedence <= precedence)
                    break;

                var op = Step();
                var right = ParseOperationExpression(binaryPrecedence);

                left = new BinaryOperationExpression(left, op, right);
            }

            return left;
        }

        private Expression ParsePrimaryExpression() {
            Expression? expr = null;

            if (TryMatch(TokenKind.LEFTPARENTHESIS, out var left)) {
                var expression = ParseExpression();
                var right = Match(TokenKind.RIGHTPARENTHESIS);
                expr = new ParenthesizedExpression(left, expression, right);
            } else if (TryMatch(TokenKind.INTEGER, out var intToken))
                expr = new IntegerLiteral(intToken);
            else if (TryMatch(TokenKind.DECIMAL, out var decimalNode))
                expr = new Nodes.DecimalLiteral(decimalNode);
            else if (TryMatch(TokenKind.TRUE, out var trueToken))
                expr = new BooleanLiteral(trueToken);
            else if (TryMatch(TokenKind.FALSE, out var falseToken))
                expr = new BooleanLiteral(falseToken);
            else if (TryMatch(TokenKind.STRING, out var stringToken))
                expr = new StringLiteral(stringToken);
            else if (TryMatch(TokenKind.CHARACTER, out var characterToken))
                expr = new CharacterLiteral(characterToken);
            else if (TryMatch(TokenKind.IDENTIFIER, out var identifierToken))
                expr = new VariableLiteral(identifierToken);

            if (expr is null) {
                Diagnostics.Add(Reports.ExpectedExpression());
                expr = new();
            }

            while (true) {
                if (TryMatch(TokenKind.DOT, out var dotToken)) {
                    var member = Match(TokenKind.IDENTIFIER);
                    expr = new MemberLookupExpression(expr, dotToken, member);
                } else if (TryMatch(TokenKind.LEFTBRACKET, out var leftBracketToken)) {
                    var indexExpression = ParseExpression();
                    var rightBracketToken = Match(TokenKind.RIGHTBRACKET);
                    expr = new IndexingExpression(expr, leftBracketToken, indexExpression, rightBracketToken);
                } else
                    break;
            }

            return expr;
        }
    }
}
