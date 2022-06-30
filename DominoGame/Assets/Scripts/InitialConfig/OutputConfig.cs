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
    private string[] _outputTypes;

    // Start is called before the first frame update
    void Start() {
        
        _outputTypes = Implementations.GetOutputTypeNames();
        OutputSelector.UpdateNames(_outputTypes);
    }

    public void SendOutputType() {

        int index = OutputSelector.Index;
        string typeName = _outputTypes[index];
        var gameConfig = GetComponent<GameConfig>();

        if (typeName == "Number") gameConfig.GetImplementations<Number>(Number.Generate);
        else if (typeName == "Letter") gameConfig.GetImplementations<Letter>(Letter.Generate);

        gameObject.SetActive(false);
    }
}
