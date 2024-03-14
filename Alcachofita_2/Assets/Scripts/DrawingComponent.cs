using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] float _minDistance = 0.1f;

    #endregion

    #region References

    private LineRenderer _line;

    #endregion

    #region Properties

    private Vector3 _lastPoint;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _lastPoint = transform.position;
    }

    private void NewPoint()
    {
        //Tomamos la nueva posición del ratón para crear un nuevo punto
        Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPoint.z = 0;

        //Si hay suficiente distancia entre los puntos, añadimos el punto nuevo en la línea
        if (Vector3.Distance(_lastPoint, newPoint) > _minDistance) 
        {
            _line.positionCount++;
        }


        Debug.Log(newPoint);
    }

    // Update is called once per frame
    void Update()
    {
        NewPoint();
    }
}
