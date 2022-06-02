using DominoEngine;

Board<Number> board = new Board<Number>();
Token<Number>[] tokens = new Token<Number>[4];

tokens[0] = new Token<Number>(new Number[] { new Number(0), new Number(0) });
tokens[1] = new Token<Number>(new Number[] { new Number(0), new Number(1) });
tokens[2] = new Token<Number>(new Number[] { new Number(1), new Number(2) });
tokens[3] = new Token<Number>(new Number[] { new Number(2), new Number(2) });

AditiveEvaluator<Number> evaluator = new AditiveEvaluator<Number>();

System.Console.WriteLine("Testing evaluator");
for (int i = 0; i < tokens.Length; i++) {
    System.Console.WriteLine(evaluator.Evaluate(tokens[i]));
}

System.Console.WriteLine("Testing token placement");

board.PlaceToken(tokens[0]);
board.PlaceToken(tokens[1], new Number(0));
board.PlaceToken(tokens[2], new Number(1));
// board.PlaceToken(tokens[3], new Number(2));

Number[] free = board.FreeOutputs;

for (int i = 0; i < free.Length; i++) {
    System.Console.WriteLine(free[i]);
}
