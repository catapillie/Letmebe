using Letmebe.Binding;
using Letmebe.Binding.Nodes;
using System.Text;

namespace Letmebe.Evaluation     {
    internal sealed class Evaluator {
        private EvalScope scope = new();

        /// <summary>
        /// Evaluates a bound program.
        /// This can only be called if the lexing, parsing and binding have not generated any diagnostics.
        /// </summary>
        /// <param name="program">The node to evaluate</param>
        public void Evaluate(BoundProgram program) {
            EvaluateStatements(program.Statements, out _);
        }

        private object? EvaluateStatements(BoundStatement[] statements, out bool exit) {
            Dictionary<BoundLabelStatement, int> indices = new();
            exit = false;

            for (int i = 0; i < statements.Length; i++) {
                var statement = statements[i];
                if (statement is BoundLabelStatement label)
                    indices[label] = i;
            }

            for (int i = 0; i < statements.Length; i++) {
                var statement = statements[i];

                switch (statement) {
                    case BoundGotoStatement gotoStatement: {
                        i = indices[gotoStatement.Label];
                        continue;
                    }

                    case BoundConditionalGotoStatement conditionalGotoStatement: {
                        bool? condition = (bool?)EvaluateExpression(conditionalGotoStatement.Condition);
                        if (condition.HasValue) {
                            bool jump = condition.Value;

                            if (jump ^ conditionalGotoStatement.Negated)
                                i = indices[conditionalGotoStatement.Label];
                        }
                        continue;
                    }

                    default:
                        var result = EvaluateStatement(statement, out exit);
                        if (exit)
                            return result;
                        break;
                }
            }
            return null;
        }

        private object? EvaluateStatement(BoundStatement statement, out bool exit) {
            exit = false;
            switch (statement) {
                case BoundBlockStatement s: {
                    ++scope;
                    var result = EvaluateStatements(s.Body, out exit);
                    --scope;
                    return result;
                }

                case BoundReturnStatement s: {
                    exit = true;
                    return EvaluateExpression(s.Expression);
                }

                case BoundVoidReturnStatement: {
                    exit = true;
                    break;
                }

                case BoundExpressionStatement s: {
                    return EvaluateExpression(s.Expression);
                }

                case BoundVariableDefinitionStatement s: {
                    scope.DeclareVariable(s.Variable, EvaluateExpression(s.Value));
                    break;
                }

                case BoundFunctionDefinitionStatement s: {
                    scope.DefineFunction(s.Symbol, s.Body);
                    break;
                }
            }
            return null;
        }

        private object? EvaluateExpression(BoundExpression expression) {
            switch (expression) {
                case BoundLiteral e: {
                    return e.Value;
                }

                case BoundSymbol e: {
                    return scope[e];
                }

                case BoundArrayExpression e: {
                    return e.Values.Select(v => EvaluateExpression(v)).ToArray();
                }

                case BoundBinaryOperation e: {
                    return EvaluateBinaryOperation(e);
                }

                case BoundUnaryOperation e: {
                    return EvaluateUnaryOperation(e);
                }

                case BoundIndexingExpression e: {
                    return EvaluateIndexingExpression(e);
                }

                case BoundAssignmentExpression s: {
                    var value = EvaluateExpression(s.Value);
                    if (s.Target is BoundSymbol symbol)
                        scope[symbol] = value;
                    return value;
                }

                case BoundFunctionCall s: {
                    if (EvaluateBuiltInFunction(s, out var value))
                        return value;

                    var body = scope[s.Function];
                    if (body != null) {
                        ++scope;

                        for (int i = 0; i < s.Parameters.Length; ++i) {
                            var p = s.Parameters[i];
                            var symbol = s.Function.ParameterSymbols[i];
                            scope.DeclareVariable(symbol, EvaluateExpression(p));
                        }
                        var result = EvaluateStatement(body, out _);

                        --scope;

                        return result;
                    }
                    break;
                }
            }

            return null;
        }

        private object? EvaluateBinaryOperation(BoundBinaryOperation operation) {
            var op = operation.Operator.Op;

            var left = EvaluateExpression(operation.Left)!;

            // Special case of lazy-evaluating the operand on the right
            if (op == BoundBinaryOperator.Operation.LogicalLazyAnd) {
                if ((bool)left)
                    return (bool)EvaluateExpression(operation.Right)!;
                return false;
            }
            if (op == BoundBinaryOperator.Operation.LogicalLazyOr) {
                if (!(bool)left)
                    return (bool)EvaluateExpression(operation.Right)!;
                return true;
            }

            var right = EvaluateExpression(operation.Right)!;

            return op switch {
                BoundBinaryOperator.Operation.IntegerAddition => (int)left + (int)right,
                BoundBinaryOperator.Operation.IntegerSubtraction => (int)left - (int)right,
                BoundBinaryOperator.Operation.IntegerMultiplication => (int)left * (int)right,
                BoundBinaryOperator.Operation.IntegerDivision => (int)left / (int)right,
                BoundBinaryOperator.Operation.IntegerExponentiation => (float)Math.Pow((int)left, (int)right),
                BoundBinaryOperator.Operation.IntegerEquality => (int)left == (int)right,
                BoundBinaryOperator.Operation.IntegerInequality => (int)left != (int)right,
                BoundBinaryOperator.Operation.IntegerLessThan => (int)left < (int)right,
                BoundBinaryOperator.Operation.IntegerGreaterThan => (int)left > (int)right,
                BoundBinaryOperator.Operation.IntegerLessThanOrEqual => (int)left <= (int)right,
                BoundBinaryOperator.Operation.IntegerGreaterThanOrEqual => (int)left >= (int)right,

                BoundBinaryOperator.Operation.FloatAddition => (float)left + (float)right,
                BoundBinaryOperator.Operation.FloatSubtraction => (float)left - (float)right,
                BoundBinaryOperator.Operation.FloatMultiplication => (float)left * (float)right,
                BoundBinaryOperator.Operation.FloatDivision => (float)left / (float)right,
                BoundBinaryOperator.Operation.FloatExponentiation => (float)Math.Pow((float)left, (float)right),
                BoundBinaryOperator.Operation.FloatEquality => (float)left == (float)right,
                BoundBinaryOperator.Operation.FloatInequality => (float)left != (float)right,
                BoundBinaryOperator.Operation.FloatLessThan => (float)left < (float)right,
                BoundBinaryOperator.Operation.FloatGreaterThan => (float)left > (float)right,
                BoundBinaryOperator.Operation.FloatLessThanOrEqual => (float)left <= (float)right,
                BoundBinaryOperator.Operation.FloatGreaterThanOrEqual => (float)left >= (float)right,

                BoundBinaryOperator.Operation.IntegerFloatAddition => (int)left + (float)right,
                BoundBinaryOperator.Operation.IntegerFloatSubtraction => (int)left - (float)right,
                BoundBinaryOperator.Operation.IntegerFloatMultiplication => (int)left * (float)right,
                BoundBinaryOperator.Operation.IntegerFloatDivision => (int)left / (float)right,
                BoundBinaryOperator.Operation.IntegerFloatExponentiation => (float)Math.Pow((int)left, (float)right),
                BoundBinaryOperator.Operation.IntegerFloatEquality => (int)left == (float)right,
                BoundBinaryOperator.Operation.IntegerFloatInequality => (int)left != (float)right,
                BoundBinaryOperator.Operation.IntegerFloatLessThan => (int)left < (float)right,
                BoundBinaryOperator.Operation.IntegerFloatGreaterThan => (int)left > (float)right,
                BoundBinaryOperator.Operation.IntegerFloatLessThanOrEqual => (int)left <= (float)right,
                BoundBinaryOperator.Operation.IntegerFloatGreaterThanOrEqual => (int)left >= (float)right,

                BoundBinaryOperator.Operation.FloatIntegerAddition => (float)left + (int)right,
                BoundBinaryOperator.Operation.FloatIntegerSubtraction => (float)left - (int)right,
                BoundBinaryOperator.Operation.FloatIntegerMultiplication => (float)left * (int)right,
                BoundBinaryOperator.Operation.FloatIntegerDivision => (float)left / (int)right,
                BoundBinaryOperator.Operation.FloatIntegerExponentiation => (float)Math.Pow((float)left, (int)right),
                BoundBinaryOperator.Operation.FloatIntegerEquality => (float)left == (int)right,
                BoundBinaryOperator.Operation.FloatIntegerInequality => (float)left != (int)right,
                BoundBinaryOperator.Operation.FloatIntegerLessThan => (float)left < (int)right,
                BoundBinaryOperator.Operation.FloatIntegerGreaterThan => (float)left > (int)right,
                BoundBinaryOperator.Operation.FloatIntegerLessThanOrEqual => (float)left <= (int)right,
                BoundBinaryOperator.Operation.FloatIntegerGreaterThanOrEqual => (float)left >= (int)right,

                BoundBinaryOperator.Operation.LogicalAnd => (bool)left && (bool)right,
                BoundBinaryOperator.Operation.LogicalOr => (bool)left || (bool)right,
                //BoundBinaryOperator.Operation.LogicalLazyAnd => ...,
                //BoundBinaryOperator.Operation.LogicalLazyOr => ...,
                BoundBinaryOperator.Operation.LogicalXor => (bool)left ^ (bool)right,

                BoundBinaryOperator.Operation.StringConcatenation => (string)left + (string)right,
                BoundBinaryOperator.Operation.StringEquality => (string)left == (string)right,
                BoundBinaryOperator.Operation.StringInequality => (string)left != (string)right,
                BoundBinaryOperator.Operation.StringLessThan => ((string)left).CompareTo((string)right) < 0,
                BoundBinaryOperator.Operation.StringGreaterThan => ((string)left).CompareTo((string)right) > 0,
                BoundBinaryOperator.Operation.StringLessThanOrEqual => ((string)left).CompareTo((string)right) <= 0,
                BoundBinaryOperator.Operation.StringGreaterThanOrEqual => ((string)left).CompareTo((string)right) >= 0,

                BoundBinaryOperator.Operation.StringRepetition => new StringBuilder().Insert(0, (string)left, (int)right).ToString(),

                BoundBinaryOperator.Operation.CharacterConcatenation => ((char)left).ToString() + ((char)right).ToString(),
                BoundBinaryOperator.Operation.CharacterEquality => (char)left == (char)right,
                BoundBinaryOperator.Operation.CharacterInequality => (char)left != (char)right,
                BoundBinaryOperator.Operation.CharacterLessThan => (char)left < (char)right,
                BoundBinaryOperator.Operation.CharacterGreaterThan => (char)left > (char)right,
                BoundBinaryOperator.Operation.CharacterLessThanOrEqual => (char)left <= (char)right,
                BoundBinaryOperator.Operation.CharacterGreaterThanOrEqual => (char)left >= (char)right,
                
                BoundBinaryOperator.Operation.CharacterRepetition => new string((char)left, (int)right),
                
                BoundBinaryOperator.Operation.CharacterStringConcatenation => ((char)left).ToString() + (string)right,
                BoundBinaryOperator.Operation.CharacterStringEquality => ((char)left).ToString() == (string)right,
                BoundBinaryOperator.Operation.CharacterStringInequality => ((char)left).ToString() != (string)right,
                BoundBinaryOperator.Operation.CharacterStringLessThan => ((char)left).ToString().CompareTo((string)right) < 0,
                BoundBinaryOperator.Operation.CharacterStringGreaterThan => ((char)left).ToString().CompareTo((string)right) > 0,
                BoundBinaryOperator.Operation.CharacterStringLessThanOrEqual => ((char)left).ToString().CompareTo((string)right) <= 0,
                BoundBinaryOperator.Operation.CharacterStringGreaterThanOrEqual => ((char)left).ToString().CompareTo((string)right) >= 0,
                
                BoundBinaryOperator.Operation.StringCharacterConcatenation => (string)left + ((char)right).ToString(),
                BoundBinaryOperator.Operation.StringCharacterEquality => (string)left == ((char)right).ToString(),
                BoundBinaryOperator.Operation.StringCharacterInequality => (string)left != ((char)right).ToString(),
                BoundBinaryOperator.Operation.StringCharacterLessThan => ((string)left).CompareTo(((char)right).ToString()) < 0,
                BoundBinaryOperator.Operation.StringCharacterGreaterThan => ((string)left).CompareTo(((char)right).ToString()) > 0,
                BoundBinaryOperator.Operation.StringCharacterLessThanOrEqual => ((string)left).CompareTo(((char)right).ToString()) <= 0,
                BoundBinaryOperator.Operation.StringCharacterGreaterThanOrEqual => ((string)left).CompareTo(((char)right).ToString()) >= 0,
                
                BoundBinaryOperator.Operation.ObjectEquality => left == right,
                BoundBinaryOperator.Operation.ObjectInequality => left != right,
                
                //BoundBinaryOperator.Operation.TypeEquality => ,
                //BoundBinaryOperator.Operation.TypeInequality => ,

                _ => null,
            };
        }

        private object? EvaluateUnaryOperation(BoundUnaryOperation operation) {
            var operand = EvaluateExpression(operation.Operand)!;

            return operation.Operator.Op switch {
                BoundUnaryOperator.Operation.Identity => operand,
                BoundUnaryOperator.Operation.IntegerNegation => -(int)operand,
                BoundUnaryOperator.Operation.FloatNegation => -(float)operand,
                BoundUnaryOperator.Operation.BooleanNegation => !(bool)operand,
                _ => null,
            };
        }

        private object? EvaluateIndexingExpression(BoundIndexingExpression indexing) {
            var operand = EvaluateExpression(indexing.Expression)!;
            var index = EvaluateExpression(indexing.IndexExpression)!;

            return indexing.Operator.Op switch {
                BoundIndexerOperator.Operation.StringIndexing => ((string)operand)[(int)index],
                BoundIndexerOperator.Operation.ArrayIndexing => ((object?[])operand)[(int)index],
                _ => null,
            };
        }

        private bool EvaluateBuiltInFunction(BoundFunctionCall call, out object? value) {
            value = null;
            var function = call.Function;

            if (function == BuiltInFunctions.PrintFunction) {
                Console.WriteLine(EvaluateExpression(call.Parameters[0])); 
                return true;
            }

            if (function == BuiltInFunctions.AskFunction) {
                Console.Write(EvaluateExpression(call.Parameters[0]));
                value = Console.ReadLine() ?? string.Empty;
                return true;
            }

            return false;
        }
    }
}
