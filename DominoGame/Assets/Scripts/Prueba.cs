using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.Effects;
using DominoEngine.Utils.Evaluators;
using DominoEngine.Utils.Filters;
using DominoEngine.Utils.Strategies;
using DominoEngine.Utils.TokenTypes;
using DominoEngine.Utils.VictoryCriteria;


public class Prueba : MonoBehaviour
{   

    // Start is called before the first frame update
    void Start() {
        string[] outputTypes = Implementations.GetOutputTypeNames();

        // System.Console.Write("Available token output types: ");
        // for (int i = 0; i < outputTypes.Length; i++) {
        //     Debug.Log(i + " - " + outputTypes[i].Item1);
        // }

        Type[] managers = Implementations.GetGameManagers();

        if ("Number" == "Number") {

            Tuple<string, strategy<Number>>[] strats = Implementations.GetStrategies<Number>();
            Tuple<string, evaluator<Number>>[] evals = Implementations.GetEvaluators<Number>();
            Tuple<string, victoryCriteria<Number>>[] criteria = Implementations.GetCriteria<Number>();
            Tuple<string, tokenFilter<Number>>[] filters = Implementations.GetFilters<Number>();
            Tuple<string, Action<IGameManager<Number>>>[] effects = Implementations.GetEffects<Number>();

            IGameManager<Number> manag = CreateGame<Number>(Number.Generate, managers); // Juego por defecto para testeo rapido
            // GameManager<Number> manag = CreateGame<Number>(Number.Generate, managers strats, evals, criteria, filters); // Permite al usuario configurar el juego
            Debug.Log("Aca");
            GameFlow(manag);
        }

        else if ("Letter" == "Letter") {

            Tuple<string, strategy<Letter>>[] strats = Implementations.GetStrategies<Letter>();
            Tuple<string, evaluator<Letter>>[] evals = Implementations.GetEvaluators<Letter>();
            Tuple<string, victoryCriteria<Letter>>[] criteria = Implementations.GetCriteria<Letter>();
            Tuple<string, tokenFilter<Letter>>[] filters = Implementations.GetFilters<Letter>();
            Tuple<string, Action<IGameManager<Letter>>>[] effects = Implementations.GetEffects<Letter>();

        }
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
        Debug.Log("Aqui");
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
                // new Power<T>(Filters<T>.AllDifferents, Effects<T>.DominunoSkip),
                // new Power<T>(Filters<T>.SameParity, Effects<T>.DominunoFlip),
                // new Power<T>(Filters<T>.AllEquals, Effects<T>.DominunoGiveTwoTokens)
            }), // Powers
        })!;
    }

    void GameFlow<T>(IGameManager<T> manager) where T : IEvaluable {

        Debug.Log("✅ Tokens dealed");
        PrintHands(manager);

        while(true) {

            var playData = manager.MakeMove();
            string msg = playData.PlayerName + ": ";
            if (playData.Token == null) msg += "Pass";
            else msg += playData.Token;
            Debug.Log(msg);

            if(playData.WinnersName != null){ 
                if (playData.WinnersName.Length == 0) Debug.Log("No one won the game");
                else {
                    for (int j = 0; j < playData.WinnersName.Length; j++)
                    {
                        Debug.Log(playData.WinnersName[j] + " has won the game!");
                    }
                }
            break;
            }
        }

        Debug.Log("\nManos resultantes:");
        PrintHands(manager);
    }

    void PrintHands<T>(IGameManager<T> manager) where T : IEvaluable {

            var finalHands = manager.PlayersTokens;

            foreach (var player in finalHands) {

                string msg = player.Item1 + ": ";
                foreach (var token in player.Item2) {
                    msg += token + " ";
                }
                Debug.Log(msg);
            }
    }
}
