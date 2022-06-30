using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HistoryOutput : MonoBehaviour
{

    public GameObject LogPrefab;
    public Scrollbar scrollbar;

    public void AddLog(string message) {

        StartCoroutine(Add(message));
    }

    private IEnumerator Add(string message) {

        var log = Instantiate(LogPrefab, transform);
        log.GetComponent<TextMeshProUGUI>().text = message;

        yield return new WaitForSeconds(0.1f);
        scrollbar.value = 0;
    }
}
