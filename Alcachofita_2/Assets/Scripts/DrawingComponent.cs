using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainTools;

public class DrawingComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] float _minDistance = 0.1f;

    #endregion


    #region Properties

    private Vector3 _lastPoint;
    private Vector3[] _positions;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _lastPoint = transform.position;
    }

    //Dibuja un trazo mientras se mantenga pulsado
    public LineRenderer Paint(Vector3 newPoint)
    {
        LineRenderer line = new LineRenderer();
        line = GetComponent<LineRenderer>();
        newPoint.z = 0;

        //Si hay suficiente distancia entre los puntos, a�adimos el punto nuevo en la l�nea
        if (Vector3.Distance(_lastPoint, newPoint) > _minDistance) 
        {
            //Suma punto
            line.positionCount++;
            //Lo a�ade a la linea
            line.SetPosition(line.positionCount - 1, newPoint); 
            //El ultimo punto dibujado es el nuevo del cursor
            _lastPoint = newPoint;
        }

        return line;
    }

    public void VariasLineas()
    {
        GameObject Lines = new GameObject();
        Lines.AddComponent<LineRenderer>();
        Lines.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
    }

    //Saca los puntos de una l�nea
    private void SacaPuntos(LineRenderer line)
    {
        line.GetPositions(_positions);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
