using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DominoEngine;

public class AcceptButton : MonoBehaviour
{

    public GameConfig GameManager;

    public void AddHandler<T>() where T : IEvaluable {
        GetComponent<Button>().onClick.AddListener(AcceptClicked<T>);
    }

    void AcceptClicked<T>() where T : IEvaluable {

        GameManager.GetUserInputs<T>();
    }
}
