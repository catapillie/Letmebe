using Letmebe.Binding;
using Letmebe.Binding.Nodes;
using Letmebe.Diagnostics;
using Letmebe.Evaluation;
using Letmebe.Lexing;
using Letmebe.Parsing;
using Letmebe.Parsing.Nodes;


Evaluator evaluator = new();

Console.ForegroundColor = ConsoleColor.White;
while (true) {
    string input = string.Empty;
    while (true) {
        Console.Write("» ");
        string? line = Console.ReadLine();
        if (string.IsNullOrEmpty(line))
            break;
        input += line + "\n";
    }
    if (string.IsNullOrEmpty(input))
        continue;

    Parser parser = new Parser(input);
    Binder binder = new Binder(parser.Diagnostics);


    Console.WriteLine();
    var program = parser.Parse();
    Display(program);
    Console.WriteLine();

    var boundProgram = binder.Bind(program);
    DisplayBound(boundProgram);
    Console.WriteLine();


    if (parser.Diagnostics.Any()) {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (Diagnostic d in parser.Diagnostics)
            Console.WriteLine(d);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    } else {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Ok");
        Console.ForegroundColor = ConsoleColor.White;
    }

    // evaluating whether diagnostics were generated or not, will move to else block above
    evaluator.Evaluate(boundProgram);
}

static void Display(SyntaxNode node, string indent = "") {
    if (node is null)
        return;

    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine(indent + node.GetType().Name);
    Console.ForegroundColor = ConsoleColor.White;

    string nextIndent = indent + "    ";
    foreach (object o in node.Children()) {
        if (o is SyntaxNode child)
            Display(child, indent: nextIndent);
        else if (o is Token token){
            Console.ForegroundColor = token.Str == null ? ConsoleColor.DarkGray : ConsoleColor.Gray;
            Console.WriteLine(nextIndent + token.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

static void DisplayBound(BoundNode node, string indent = "") {
    if (node is null)
        return;

    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine(indent + node.GetType().Name);
    Console.ForegroundColor = ConsoleColor.White;

    string nextIndent = indent + "    ";
    foreach (object o in node.Children()) {
        if (o is BoundNode child)
            DisplayBound(child, indent: nextIndent);
        else if (o is not null) {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(nextIndent + o.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}