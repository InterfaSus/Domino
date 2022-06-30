using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{   

    public TextMeshProUGUI ValueText;

    void Start() {
        UpdateValue();
    }

    public void UpdateValue() {
        
        ValueText.text = gameObject.GetComponent<Slider>().value.ToString();
    }
}
