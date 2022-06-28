using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectorController : MonoBehaviour
{

    public TextMeshProUGUI DisplayText;
    private string[] ImplementationsNames;
    public int Index { get; private set; } = 0;
    public string Current { get => ImplementationsNames[Index]; }

    public void MoveType(int dir) {

        Index = (Index + dir + ImplementationsNames.Length) % ImplementationsNames.Length;
        DisplayText.text = ImplementationsNames[Index];
    }

    public void UpdateNames(string[] names) {
        
        ImplementationsNames = names;
        DisplayText.text = ImplementationsNames[Index];
    }
}
