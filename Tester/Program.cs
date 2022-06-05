using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;

strategy<Number>[] playerStrategies = new strategy<Number>[4];

playerStrategies[0] = Strategies<Number>.BiggestOption;
playerStrategies[1] = Strategies<Number>.BiggestOption;
playerStrategies[2] = Strategies<Number>.BiggestOption;
playerStrategies[3] = Strategies<Number>.BiggestOption;

GameManager<Number> manager = new GameManager<Number>(playerStrategies, Number.Generate, tokenTypeAmount: 7, tokensInHand: 7, outputsAmount: 2);

System.Console.WriteLine("✅ Tokens dealed");


for (int i = 0; i < 40; i++) {

    Console.Write($"Player {i % 4 + 1}: ");

    var (token, output) = manager.MakeMove();
    if (token == null) System.Console.WriteLine("Pass");
    else System.Console.WriteLine(token);
}