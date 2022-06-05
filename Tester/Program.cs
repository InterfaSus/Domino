using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;

strategy<Number>[] playerStrategies = new strategy<Number>[4];

playerStrategies[0] = Strategies<Number>.BiggestOption;
playerStrategies[1] = Strategies<Number>.BiggestOption;
playerStrategies[2] = Strategies<Number>.BiggestOption;
playerStrategies[3] = Strategies<Number>.BiggestOption;

GameManager<Number> manager = new GameManager<Number>(playerStrategies, Number.Generate, tokenTypeAmount: 7, tokensInHand: 7, outputsAmount: 2, playerNames: new string[]{"Juan", "El Pepe", "Ete sech", "Potaxio"});

System.Console.WriteLine("✅ Tokens dealed");


for (int i = 0; i < 40; i++) {

    var (name, token, output) = manager.MakeMove();
    Console.Write(name + ": ");
    if (token == null) System.Console.WriteLine("Pass");
    else System.Console.WriteLine(token);
}