using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputsShow : MonoBehaviour
{

    public GameObject DisplayPrefab;
    private float _height;

    public void RenderOutputs<T>(KeyValuePair<T, int>[] outputs) {

        GameObject[] displays = GameObject.FindGameObjectsWithTag("Output Display");
        foreach (var item in displays) {
            Destroy(item);
        }

        int n = outputs.Length;
        float angle = Mathf.PI * 2 / (float)n;

        for (int i = 0; i < n; i++) {
            
            float a = i * angle;

            float x = (_height + _height / (float)3)/ (float)2 * Mathf.Cos(a) + transform.position.x;
            float y = (_height + _height / (float)3)/ (float)2 * Mathf.Sin(a) + transform.position.y;

            GameObject newDisplay = Instantiate(DisplayPrefab, new Vector3(x, y, 0), new Quaternion(), transform);
            newDisplay.GetComponent<OutputDisplayController>().ShowOutput(outputs[i].Key.ToString(), outputs[i].Value);
        }
    }

    void Update() {
        _height = gameObject.GetComponent<RectTransform>().rect.height - 10;
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(_height, _height);
    }
}
