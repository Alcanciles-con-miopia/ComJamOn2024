using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    #region Parameters

    [SerializeField] private LineRenderer _line;

    #endregion

    #region References

    private DrawingComponent _drawingComponent;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (_line != null) _drawingComponent = _line.GetComponent<DrawingComponent>();
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
            _drawingComponent.VariasLineas();
        }

        if (Input.GetMouseButton(0))

        {
            Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _drawingComponent.Paint(newPoint);
            Debug.Log("Mira mamá, sé pintar");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("boton derecho");
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("boton medio");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (GameManager.Instance != null) 
                GameManager.Instance.QuitaDedo();
            Debug.Log("Borra eso");
        }
    }
}
