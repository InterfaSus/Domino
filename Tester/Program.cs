using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;

strategy<Number>[] playerStrategies = new strategy<Number>[4];

playerStrategies[0] = Strategies<Number>.RandomOption;
playerStrategies[1] = Strategies<Number>.RandomOption;
playerStrategies[2] = Strategies<Number>.RandomOption;
playerStrategies[3] = Strategies<Number>.RandomOption;

GameManager<Number> manager = new GameManager<Number>(playerStrategies, Number.Generate, tokenTypeAmount: 9, tokensInHand: 10);

System.Console.WriteLine("OK");