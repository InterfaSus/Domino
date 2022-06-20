using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Effects;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Filters;
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
    (string, tokenFilter<Number>)[] filters = Implementations.GetFilters<Number>();
    Type[] managers = Implementations.GetGameManagers<Number>();
    (string, Action<IGameManager<Number>>)[] effects = Implementations.GetEffects<Number>();

    IGameManager<Number> manag = CreateGame<Number>(Number.Generate, managers); // Juego por defecto para testeo rapido
    // GameManager<Number> manag = CreateGame<Number>(Number.Generate, managers strats, evals, criteria, filters); // Permite al usuario configurar el juego
    GameFlow(manag);
}

IGameManager<T> CreateGame<T>(Generator<T> generator, Type[] managers) where T : IEvaluable {

    strategy<T>[] playerStrategies = new strategy<T>[4];

    playerStrategies[0] = Strategies<T>.BiggestOption;
    playerStrategies[1] = Strategies<T>.BiggestOption;
    playerStrategies[2] = Strategies<T>.BiggestOption;
    playerStrategies[3] = Strategies<T>.BiggestOption;

    VictoryChecker<T> Checker = new VictoryChecker<T>(VictoryCriteria<T>.SurpassSumCriteria, null, 230); 

    CriteriaCollection<T> collection = new CriteriaCollection<T>(new VictoryChecker<T> (VictoryCriteria<T>.DefaultCriteria));
    collection.Add(Checker);

    evaluator<T> ev = Evaluators<T>.AdditiveEvaluator;

    var generic = managers[1].MakeGenericType(typeof(T));
    return (IGameManager<T>)Activator.CreateInstance(generic, new object[] {
        playerStrategies, // Strategies
        generator, // Generator
        10, // tokenTypeAmount
        10, // tokensInHand
        2, // outputsAmount
        new string[]{"Juan", "El Pepe", "Ete sech", "Potaxio"}, // playerNames
        ev, // Evaluator
        collection, // VictoryCheckerCollection
        new Powers<T>(new Power<T>[]{ 
            new Power<T>(Filters<T>.AllDifferents, Effects<T>.DominunoSkip),
            new Power<T>(Filters<T>.SameParity, Effects<T>.DominunoFlip),
            new Power<T>(Filters<T>.AllEquals, Effects<T>.DominunoGiveTwoTokens)
        }), // Powers
    })!;
}

void GameFlow<T>(IGameManager<T> manager) where T : IEvaluable {

    System.Console.WriteLine("✅ Tokens dealed");
    PrintHands(manager);

    while(true) {

        var (name, token, output, winners) = manager.MakeMove();
        Console.Write(name + ": ");
        if (token == null) System.Console.WriteLine("Pass");
        else System.Console.WriteLine(token);

        if(winners != null){ 
            if (winners.Length == 0) System.Console.WriteLine("No one won the game");
            else {
                for (int j = 0; j < winners.Length; j++)
                {
                    System.Console.WriteLine($"{winners[j]} has won the game!");
                }
            }
        break;
        }
    }

    System.Console.WriteLine("\nManos resultantes:");
    PrintHands(manager);
}

void PrintHands<T>(IGameManager<T> manager) where T : IEvaluable {

        var finalHands = manager.PlayersTokens;

        foreach (var player in finalHands) {

            Console.Write(player.Item1 + ": ");
            foreach (var token in player.Item2) {
                Console.Write(token + " ");
            }
            System.Console.WriteLine();
        }
}