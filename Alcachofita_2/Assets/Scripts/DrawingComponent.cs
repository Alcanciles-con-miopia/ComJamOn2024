using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TerrainTools;

public class DrawingComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] float _minDistance = 0.1f;

    #endregion

    #region Properties

    private Vector3 _lastPoint;
    private Vector3[] _positions;
    private Coroutine _drawing;

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

        //Creamos un hijo por cada l�nea que pintemos
        line = new GameObject().AddComponent<LineRenderer>();
        line.transform.SetParent(transform);

        //Creamos una lista para a�adir los puntos (luego lo pasamos a array)
        List<Vector3> puntos = new List<Vector3>();

        //A�adimos los puntos a la lista
        for (int i = 0; i < line.positionCount; i++)
        {
            puntos.Add(line.GetPosition(i));
        }

        line.SetPositions(puntos.ToArray());

        return line;
    }

    //public void VariasLineas(LineRenderer line)
    //{
    //    //Creamos un hijo por cada l�nea que pintemos
    //    line = new GameObject().AddComponent<LineRenderer>();
    //    line.transform.SetParent(transform);

    //    //Creamos una lista para a�adir los puntos (luego lo pasamos a array)
    //    List<Vector3> puntos = new List<Vector3>();
        
    //    //A�adimos los puntos a la lista
    //    for (int i = 0 ; i < line.positionCount; i++) 
    //    {
    //        puntos.Add(line.GetPosition(i));
    //    }

    //    line.SetPositions(puntos.ToArray());
    //}

    //public void StartLine(Vector3 newPoint)
    //{
    //    newPoint.z = 0;
    //    _drawing = StartCoroutine(Paint(newPoint));
    //}

    //public void FinishLine()
    //{
    //    StopCoroutine(_drawing);
    //}


}
