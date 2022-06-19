using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;
using DominoEngine.Utils.VictoryCriteria;

(string, Type)[] outputTypes = Implementations.GetOutputTypes();

System.Console.Write("Available token output types: ");
for (int i = 0; i < outputTypes.Length; i++) {
    System.Console.WriteLine($"{i} - {outputTypes[i].Item1}");
}
int ind  = int.Parse((Console.ReadLine()!));

if (outputTypes[ind].Item1 == "Number") {

    (string, strategy<Number>)[] strats = Implementations.GetStrategies<Number>();
    (string, evaluator<Number>)[] evals = Implementations.GetEvaluators<Number>();
    (string, victoryCriteria<Number>)[] criteria = Implementations.GetCriteria<Number>();

    GameManager<Number> manag = CreateGame<Number>(Number.Generate); // Juego por defecto para testeo rapido
    // GameManager<Number> manag = CreateGame<Number>(Number.Generate, strats, evals, criteria); // Permite al usuario configurar el juego
    GameFlow(manag);
}

GameManager<T> CreateGame<T>(Generator<T> generator) where T : IEvaluable {

    strategy<T>[] playerStrategies = new strategy<T>[4];

    playerStrategies[0] = Strategies<T>.BiggestOption;
    playerStrategies[1] = Strategies<T>.BiggestOption;
    playerStrategies[2] = Strategies<T>.BiggestOption;
    playerStrategies[3] = Strategies<T>.BiggestOption;

    VictoryChecker<T> Checker = new VictoryChecker<T>(VictoryCriteria<T>.SurpassSumCriteria, null, 230); 

    CriteriaCollection<T> collection = new CriteriaCollection<T>(new VictoryChecker<T> (VictoryCriteria<T>.DefaultCriteria));
    collection.Add(Checker);

    return new GameManager<T>(playerStrategies, 
                              generator,
                              tokenTypeAmount: 7,
                              tokensInHand: 7,
                              outputsAmount: 2,
                              playerNames: new string[]{"Juan", "El Pepe", "Ete sech", "Potaxio"},
                              evaluator: Evaluators<T>.AdditiveEvaluator,
                              victoryCheckerCollection: collection);
}

// Para que el usuario configure el juego
// NO IMPLEMENTAR. Hay que hacerlo de 0 en Unity
// GameManager<T> CreateGame<T>(Generator<T> generator, (string, strategy<T>)[] strats, (string, evaluator<T>)[] evals, (string, victoryCriteria<T>)[] criteria) where T : IEvaluable {

// }

void GameFlow<T>(GameManager<T> manager) where T : IEvaluable {

    System.Console.WriteLine("✅ Tokens dealed");

    while(true) {

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
}