using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DominoEngine;

public class AutoMoveButton : MonoBehaviour
{
    public GameFlow gameFlow;

    private Coroutine routine;

    public void AddHandler<T>() where T : IEvaluable {
        GetComponent<Button>().onClick.AddListener(ButtonClicked<T>);
    }

    void ButtonClicked<T>() where T : IEvaluable {
        
        if (gameFlow.Autoplaying) {

            gameFlow.Autoplaying = false;
            StopCoroutine(routine);
        }
        else routine = StartCoroutine(gameFlow.AutoPlay<T>());
    }
}
