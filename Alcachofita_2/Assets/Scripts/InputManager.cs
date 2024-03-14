using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("boton izquierdo");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("boton derecho");
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("boton medio");
        }
    }
}
