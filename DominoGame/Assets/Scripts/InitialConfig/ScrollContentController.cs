using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContentController : MonoBehaviour
{

    public GameObject elementPrefab;
    private string[] _implementations;

    public string[] Currents { 
        get {

            string[] result = new string[transform.childCount];

            for (int i = 0; i < transform.childCount; i++) {

                var selector = transform.GetChild(i).gameObject.GetComponentInChildren<SelectorController>();
                result[i] = selector.Current;
            }

            return result;
        }
    }

    void Start()
    {   
        for (int i = 0; i < transform.childCount; i++) {
            var selector = transform.GetChild(i).gameObject.GetComponentInChildren<SelectorController>();
            selector.UpdateNames(_implementations);
        }
    }

    public void UpdateImplementations(string[] implementations) {
        _implementations = implementations;
    }

    public void AddElement() {

        GameObject newElement = Instantiate(elementPrefab, transform);
        var selector = newElement.GetComponentInChildren<SelectorController>();
        selector.UpdateNames(_implementations);
    }

    public void RemoveElements() {

        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
