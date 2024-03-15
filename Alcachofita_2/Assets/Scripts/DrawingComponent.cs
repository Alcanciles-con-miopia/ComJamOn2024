using System.Collections.Generic;
using UnityEngine;

public class DrawingComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] float _minDistance = 0.1f;

    #endregion

    #region Properties

    private Vector3 _lastPoint;
    private Vector3[] _positions;
    private LineRenderer _lineaNueva;

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

        //Si hay suficiente distancia entre los puntos, añadimos el punto nuevo en la línea
        if (Vector3.Distance(_lastPoint, newPoint) > _minDistance)
        {
            //Suma punto
            line.positionCount++;
            //Lo añade a la linea
            line.SetPosition(line.positionCount - 1, newPoint);
            //El ultimo punto dibujado es el nuevo del cursor
            _lastPoint = newPoint;
        }

        return line;
    }

    public void VariasLineas(LineRenderer line)
    {
        //Creamos un hijo por cada línea que pintemos
        LineRenderer Lines = new GameObject().AddComponent<LineRenderer>();
        Lines.transform.SetParent(transform);

        //Creamos una lista para añadir los puntos (luego lo pasamos a array)
        List<Vector3> puntos = new List<Vector3>();

        //Añadimos los puntos a la lista
        for (int i = 0; i < line.positionCount; i++)
        {
            puntos.Add(line.GetPosition(i));
        }

        line.SetPositions(puntos.ToArray());
    }

    //Saca los puntos de una línea
    private void SacaPuntos(LineRenderer line)
    {
        line.GetPositions(_positions);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
