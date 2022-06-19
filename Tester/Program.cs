using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;
using DominoEngine.Utils.VictoryCriteria;

strategy<Number>[] playerStrategies = new strategy<Number>[4];

playerStrategies[0] = Strategies<Number>.BiggestOption;
playerStrategies[1] = Strategies<Number>.BiggestOption;
playerStrategies[2] = Strategies<Number>.BiggestOption;
playerStrategies[3] = Strategies<Number>.BiggestOption;

VictoryChecker<Number> Checker = new VictoryChecker<Number>(VictoryCriteria<Number>.SurpassSumCriteria, null, 230); 

CriteriaCollection<Number> collection = new CriteriaCollection<Number>( new VictoryChecker<Number> (VictoryCriteria<Number>.DefaultCriteria ) );
collection.Add(Checker);
GameManager<Number> manager = new GameManager<Number>(playerStrategies, Number.Generate, tokenTypeAmount: 7, tokensInHand: 7, outputsAmount: 2, playerNames: new string[]{"Juan", "El Pepe", "Ete sech", "Potaxio"}, null, collection);

System.Console.WriteLine("✅ Tokens dealed");

for (int i = 0; i < 40; i++) {

    var (name, token, output, winners) = manager.MakeMove();
    Console.Write(name + ": ");
    if (token == null) System.Console.WriteLine("Pass");
    else System.Console.WriteLine(token);

    if(winners != null && winners.Length != 0){ 
        for (int j = 0; j < winners.Length; j++)
        {
            System.Console.WriteLine($"{winners[j]} has won the game!");
        }
    
    break;
    }
}

System.Console.WriteLine("\nManos resultantes:");

var finalHands = manager.PlayersTokens;

foreach (var player in finalHands) {

    Console.Write(player.Item1 + ": ");
    foreach (var token in player.Item2) {
        Console.Write(token + " ");
    }
    System.Console.WriteLine();
}

(string, Type)[] outputTypes = Implementations.GetOutputTypes();

Type t = outputTypes[0].Item2;

// var evals = Implementations.GetEvaluators<t>;

// Tipo esperado (sring, evaluator<t>)[]. Esta devolviendo un object
var evals = typeof(Implementations).GetMethod("GetEvaluators")!.MakeGenericMethod(t).Invoke(null, null)!;

System.Console.WriteLine(evals);

(string, strategy<Number>)[] strats = Implementations.GetStrategies<Number>();
(string, victoryCriteria<Number>)[] criteria = Implementations.GetCriteria<Number>();
