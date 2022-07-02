using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DominoEngine;
using DominoEngine.Utils;
using DominoEngine.Utils.TokenTypes;

public class OutputConfig : MonoBehaviour
{   

    public SelectorController OutputSelector;
    object[] _outputTypes;

    // Start is called before the first frame update
    void Start() {
        
        _outputTypes = Implementations.GetOutputTypes();
        OutputSelector.UpdateNames(TypesNameHandler.GetNames<Generator<IEvaluable>>(_outputTypes));
    }

    public void SendOutputType() {

        string name = OutputSelector.Current;
        Generator<IEvaluable> typeGenerator = TypesNameHandler.ImplementationByName<Generator<IEvaluable>>(_outputTypes, name);

        var gameConfig = GetComponent<GameConfig>();
        gameConfig.GetImplementations(typeGenerator);

        gameObject.SetActive(false);
    }
}
