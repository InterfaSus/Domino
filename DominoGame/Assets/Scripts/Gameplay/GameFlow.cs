using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

using DominoEngine;

public class GameFlow : MonoBehaviour
{

    public GameObject GamePanel;
    public GameObject ControlPanel;
    public TextMeshProUGUI HandsText;
    public bool Autoplaying = false;
    

    private HistoryOutput _history;
    private object _gameManager;
    private bool _gameEnded = false;

    public void StartGame<T>(IGameManager<T> manager) where T : IEvaluable {

        GamePanel.SetActive(true);
        _history = GamePanel.GetComponentInChildren<HistoryOutput>();
        _gameManager = manager;

        FindObjectOfType<SingleMoveButton>().AddHandler<T>();
        FindObjectOfType<AutoMoveButton>().AddHandler<T>();

        PrintHands<T>();
    }

    public void MakeMove<T>() where T : IEvaluable {

        if (_gameEnded) return;

        var manager = (IGameManager<T>)_gameManager;

        var playData = manager.MakeMove();

        string msg = playData.PlayerName + ": ";
        if (playData.Token == null) msg += "Pass";
        else msg += playData.Token;
        
        _history.AddLog(msg);
        PrintHands<T>();
        FindObjectOfType<OutputsShow>().RenderOutputs<T>(manager.FreeOutputsAmount);

        if(playData.WinnersName != null){ 
            WinStatus<T>(playData);
        }
    }

    public void SinglePlay<T>() where T : IEvaluable {
        
        Autoplaying = false;
        MakeMove<T>();
    }

    public IEnumerator AutoPlay<T>() where T : IEvaluable {
        
        Autoplaying = true;

        while(Autoplaying && !_gameEnded) {

            yield return new WaitForSeconds(0.5f);
            MakeMove<T>();
        }
    }

    public void BackToMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void WinStatus<T>(WinnerPlayData<T> playData) where T : IEvaluable {

        if (playData.WinnersName.Length == 0) _history.AddLog("No one won");
        else {
            for (int j = 0; j < playData.WinnersName.Length; j++)
            {
                _history.AddLog(playData.WinnersName[j] + " won!");
            }
            _gameEnded = true;
            Autoplaying = false;
            ControlPanel.SetActive(false);
        }
    }

    void PrintHands<T>() where T : IEvaluable {
        
        var manager = (IGameManager<T>)_gameManager;
        var finalHands = manager.PlayersTokens;

        string msg = "";

        foreach (var player in finalHands) {

            msg += player.Item1 + ": ";
            foreach (var token in player.Item2) {
                msg += token + " ";
            }
            msg += "\n";
        }

        HandsText.text = msg;
    }
}
