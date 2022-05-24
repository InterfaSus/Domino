using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DominoEngine;

public class Prueba : MonoBehaviour
{   

    // Start is called before the first frame update
    void Start()
    {
        Ficha ficha = new Ficha(2, 3);
        Debug.Log(ficha.ToString());
        ficha.Voltear();
        Debug.Log(ficha.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
