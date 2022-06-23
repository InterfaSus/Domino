using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Effects;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Filters;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;
using DominoEngine.Utils.VictoryCriteria;

using DominoEngine.Algorithms;

string[] outputTypes = Implementations.GetOutputTypeNames();

System.Console.Write("Available token output types: ");
for (int i = 0; i < outputTypes.Length; i++) {
    System.Console.WriteLine(i + " - " + outputTypes[i]);
}
int ind  = int.Parse((Console.ReadLine()!));

    Type[] managers = Implementations.GetGameManagers();

if (outputTypes[ind] == "Number") {

    Tuple<string, strategy<Number>>[] strats = Implementations.GetStrategies<Number>();
    Tuple<string, evaluator<Number>>[] evals = Implementations.GetEvaluators<Number>();
    Tuple<string, victoryCriteria<Number>>[] criteria = Implementations.GetCriteria<Number>();
    Tuple<string, tokenFilter<Number>>[] filters = Implementations.GetFilters<Number>();
    Tuple<string, Action<IGameManager<Number>>>[] effects = Implementations.GetEffects<Number>();

    IGameManager<Number> manag = CreateGame<Number>(Number.Generate, managers); // Juego por defecto para testeo rapido
    // GameManager<Number> manag = CreateGame<Number>(Number.Generate, managers strats, evals, criteria, filters); // Permite al usuario configurar el juego
    GameFlow(manag);
}
else if (outputTypes[ind] == "Letter") {

    Tuple<string, strategy<Letter>>[] strats = Implementations.GetStrategies<Letter>();
    Tuple<string, evaluator<Letter>>[] evals = Implementations.GetEvaluators<Letter>();
    Tuple<string, victoryCriteria<Letter>>[] criteria = Implementations.GetCriteria<Letter>();
    Tuple<string, tokenFilter<Letter>>[] filters = Implementations.GetFilters<Letter>();
    Tuple<string, Action<IGameManager<Letter>>>[] effects = Implementations.GetEffects<Letter>();

    IGameManager<Letter> manag = CreateGame<Letter>(Letter.Generate, managers); // Juego por defecto para testeo rapido
    // GameManager<Letter> manag = CreateGame<Letter>(Letter.Generate, managers strats, evals, criteria, filters); // Permite al usuario configurar el juego
    GameFlow(manag);
}

IGameManager<T> CreateGame<T>(Generator<T> generator, Type[] managers) where T : IEvaluable {

    strategy<T>[] playerStrategies = new strategy<T>[4];

    playerStrategies[0] = Strategies<T>.PreventOtherPlayersFromPlaying;
    playerStrategies[1] = Strategies<T>.PreventOtherPlayersFromPlaying;
    playerStrategies[2] = Strategies<T>.PreventOtherPlayersFromPlaying;
    playerStrategies[3] = Strategies<T>.PreventOtherPlayersFromPlaying;

    VictoryChecker<T> Checker = new VictoryChecker<T>(VictoryCriteria<T>.SurpassSumCriteria, null, 230); 

    CriteriaCollection<T> collection = new CriteriaCollection<T>(new VictoryChecker<T> (VictoryCriteria<T>.DefaultCriteria));
    collection.Add(Checker);

    evaluator<T> ev = Evaluators<T>.AdditiveEvaluator;

    var generic = managers[0].MakeGenericType(typeof(T));
    return (IGameManager<T>)Activator.CreateInstance(generic, new object[] {
        playerStrategies, // Strategies
        generator, // Generator
        7, // tokenTypeAmount
        7, // tokensInHand
        2, // outputsAmount
        new string[]{"Juan", "El Pepe", "Ete sech", "Potaxio"}, // playerNames
        ev, // Evaluator
        collection, // VictoryCheckerCollection
        new Powers<T>(new Power<T>[]{ 
            // new Power<T>(Filters<T>.AllDifferent, Effects<T>.DominunoSkip),
            // new Power<T>(Filters<T>.SameParity, Effects<T>.DominunoFlip),
            // new Power<T>(Filters<T>.AllEqual, Effects<T>.DominunoGiveTwoTokens)
        }), // Powers
    })!;
}

void GameFlow<T>(IGameManager<T> manager) where T : IEvaluable {

    System.Console.WriteLine("✅ Tokens dealed");
    PrintHands(manager);

    while(true) {

        var playData = manager.MakeMove();
        Console.Write(playData.PlayerName + ": ");
        if (playData.Token == null) System.Console.WriteLine("Pass");
        else System.Console.WriteLine(playData.Token);

        if(playData.WinnersName != null){ 
            if (playData.WinnersName.Length == 0) System.Console.WriteLine("No one won the game");
            else {
                for (int j = 0; j < playData.WinnersName.Length; j++)
                {
                    System.Console.WriteLine(playData.WinnersName[j] + " has won the game!");
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