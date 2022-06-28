using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DominoEngine;

public class GameFlow : MonoBehaviour
{
    public void StartGame<T>(IGameManager<T> manager) where T : IEvaluable {

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
