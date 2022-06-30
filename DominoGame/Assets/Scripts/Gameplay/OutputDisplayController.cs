using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutputDisplayController : MonoBehaviour
{

    public TextMeshProUGUI StringValue;
    public TextMeshProUGUI Amount;

    public void ShowOutput(string value, int amount) {
        
        StringValue.text = value;
        Amount.text = amount.ToString();
    }
}
