using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowersScroll : MonoBehaviour
{   

    private string[] _powers;
    private string[] _filters;

    public Tuple<string, string>[] GetPowers {
        get {
            
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            string[] content = GetComponent<ScrollContentController>().Currents;
            Toggle[] marks = GetComponentsInChildren<Toggle>();
            GameObject[] names = GameObject.FindGameObjectsWithTag("Power Name");

            for (int i = 0; i < content.Length; i++) {

                if (marks[i].isOn) {
                    result.Add(new Tuple<string, string>(names[i].GetComponent<TextMeshProUGUI>().text, content[i]));
                }
            }

            return result.ToArray();
        }
    }

    public void GetImplementationData(string[] powers, string[] filters) {
        
        _powers = powers;
        _filters = filters;

        GetComponent<ScrollContentController>().UpdateImplementations(filters);
        GetComponent<ScrollContentController>().RemoveElements();
        
        List<string> validPowers = new List<string>();

        for (int i = 0; i < powers.Length; i++) {

            GetComponent<ScrollContentController>().AddElement();
            validPowers.Add(powers[i]);
        }

        GameObject[] powerTextField = GameObject.FindGameObjectsWithTag("Power Name");
        for (int i = powerTextField.Length - 1, j = validPowers.Count - 1; i >= powerTextField.Length - validPowers.Count; i--, j--) {
            powerTextField[i].GetComponent<TextMeshProUGUI>().text = validPowers[j];
        }
    }
}
