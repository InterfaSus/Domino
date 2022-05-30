using DominoEngine;

Board board = new Board();
Token[] tokens = new Token[4];

tokens[0] = new Token(new int[] { 0, 0 });
tokens[1] = new Token(new int[] { 0, 1 });
tokens[2] = new Token(new int[] { 1, 2 });
tokens[3] = new Token(new int[] { 2, 2 });

RegularEvaluator evaluator = new RegularEvaluator();

System.Console.WriteLine("Testing evaluator");
for (int i = 0; i < tokens.Length; i++) {
    System.Console.WriteLine(evaluator.Evaluate(tokens[i]));
}

System.Console.WriteLine("Testing token placement");

board.PlaceToken(tokens[0]);
board.PlaceToken(tokens[1], 0);
board.PlaceToken(tokens[2], 1);
board.PlaceToken(tokens[3], 2);

int[] free = board.FreeOutputs;

for (int i = 0; i < free.Length; i++) {
    System.Console.WriteLine(free[i]);
}
