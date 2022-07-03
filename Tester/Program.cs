using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Effects;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Filters;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.VictoryCriteria;

Tuple<string, Generator<IEvaluable>>[] outputTypes = Implementations.GetOutputTypes();

System.Console.WriteLine("Available token output types: ");
for (int i = 0; i < outputTypes.Length; i++) {
    System.Console.WriteLine(i + " - " + outputTypes[i].Item1);
}
int ind  = int.Parse((Console.ReadLine()!));

// Generator<IEvaluable>[] generators = new Generator<IEvaluable>[] {Number.Generate, Letter.Generate};

GetImplementations(outputTypes[ind].Item2);

// if (outputTypes[ind] == "Number") GetImplementations(Number.Generate);
// else if (outputTypes[ind] == "Letter") GetImplementations(Letter.Generate);

void GetImplementations<T>(Generator<T> generator) where T : IEvaluable {

    Tuple<string, strategy<T>>[] strats = Implementations.GetStrategies<T>();
    Tuple<string, evaluator<T>>[] evals = Implementations.GetEvaluators<T>();
    Tuple<string, victoryCriteria<T>>[] criteria = Implementations.GetCriteria<T>();
    Tuple<string, tokenFilter<T>>[] filters = Implementations.GetFilters<T>();
    Tuple<string, Action<EffectsExecution<T>>>[] effects = Implementations.GetEffects<T>();

    GameManager<T> manag = CreateGame<T>(generator); // Juego por defecto para testeo rapido
    GameFlow(manag);
}

GameManager<T> CreateGame<T>(Generator<T> generator) where T : IEvaluable {

    strategy<T>[] playerStrategies = new strategy<T>[4];

    playerStrategies[0] = Strategies<T>.RandomOption;
    playerStrategies[1] = Strategies<T>.RandomOption;
    playerStrategies[2] = Strategies<T>.RandomOption;
    playerStrategies[3] = Strategies<T>.RandomOption;

    VictoryChecker<T> Checker = new VictoryChecker<T>(VictoryCriteria<T>.SurpassSumCriteria, 30); 

    CriteriaCollection<T> collection = new CriteriaCollection<T>(new VictoryChecker<T>(VictoryCriteria<T>.EndAtXPass, 4));
    // collection.Add(Checker);

    evaluator<T> ev = Evaluators<T>.AdditiveEvaluator;

    return new GameManager<T>(
        playerStrategies,
        generator,
        tokenTypeAmount: 7,
        tokensInHand: 7,
        outputsAmount: 2,
        evaluator: ev,
        victoryCheckerCollection: collection,
        powers: new Powers<T>(new Power<T>[]{ 
            new Power<T>(Filters<T>.AllDifferent, Effects<T>.RandomTurn),
            // new Power<T>(Filters<T>.SameParity, Effects<T>.DominunoFlip),
            // new Power<T>(Filters<T>.AllEqual, Effects<T>.DominunoGiveTwoTokens)
        }));
}

void GameFlow<T>(GameManager<T> manager) where T : IEvaluable {

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

void PrintHands<T>(GameManager<T> manager) where T : IEvaluable {

        var finalHands = manager.PlayersTokens;

        foreach (var player in finalHands) {

            Console.Write(player.Item1 + ": ");
            foreach (var token in player.Item2) {
                Console.Write(token + " ");
            }
            System.Console.WriteLine();
        }
}