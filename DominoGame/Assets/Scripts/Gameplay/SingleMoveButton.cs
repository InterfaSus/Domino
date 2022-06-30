using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DominoEngine;

public class SingleMoveButton : MonoBehaviour
{
    public GameFlow gameFlow;

    public void AddHandler<T>() where T : IEvaluable {
        GetComponent<Button>().onClick.AddListener(ButtonClicked<T>);
    }

    void ButtonClicked<T>() where T : IEvaluable {
        gameFlow.SinglePlay<T>();
    }
}
