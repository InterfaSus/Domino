using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowersScroll : MonoBehaviour
{   

    public SelectorController ManagerType;

    private string[] _powers;
    private string[] _filters;

    public Tuple<string, string>[] GetPowers {
        get {
            
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            string[] content = GetComponent<ScrollContentController>().Currents;
            Toggle[] marks = GetComponentsInChildren<Toggle>();

            for (int i = 0; i < content.Length; i++) {

                if (marks[i].isOn) {
                    result.Add(new Tuple<string, string>(_powers[i], content[i]));
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

            if (powers[i].Contains(ManagerType.Current)) {
                
                GetComponent<ScrollContentController>().AddElement();
                validPowers.Add(powers[i].Substring(ManagerType.Current.Length + 1, powers[i].Length - ManagerType.Current.Length - 1));
            }
        }

        GameObject[] powerTextField = GameObject.FindGameObjectsWithTag("Power Name");
        for (int i = 0; i < validPowers.Count; i++) {
            powerTextField[i].GetComponent<TextMeshProUGUI>().text = validPowers[i];
        }
    }

    public void MoveManagerType(int dir) {

        ManagerType.MoveType(dir);
        GetImplementationData(_powers, _filters);
    }
}
