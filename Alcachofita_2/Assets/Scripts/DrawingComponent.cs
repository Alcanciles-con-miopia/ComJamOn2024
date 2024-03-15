using System.Collections.Generic;
using UnityEngine;

public class DrawingComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] float _minDistance = 0.1f;
    [SerializeField] Material _material;
    [SerializeField] float _startWidth = 0.5f;
    [SerializeField] float _endWidth = 0.5f;

    #endregion

    #region Properties

    LineRenderer line = new LineRenderer();
    private Vector3 _lastPoint;
    private Vector3 _centralPoint;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _lastPoint = transform.position;
    }

    //Dibuja un trazo mientras se mantenga pulsado
    public LineRenderer Paint(Vector3 newPoint)
    {
        newPoint.z = 1;

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
        //Creamos un hijo por cada l�nea que pintemos
        LineRenderer newLine = new GameObject().AddComponent<LineRenderer>();
        newLine.transform.SetParent(transform);
        line = newLine.GetComponent<LineRenderer>();

        //Guarrada para los dos primeros puntos
        line.SetPosition(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        line.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Color
        line.startColor = new Color(120, 2, 2, 255);
        line.endColor = new Color(120, 2, 2, 255);

        //Materiales
        List<Material> materials = new List<Material>();
        materials.Add(_material);
        line.SetMaterials(materials);

        //Tamaño
        line.startWidth = _startWidth;
        line.endWidth = _endWidth;

        //Creamos una lista para a�adir los puntos (luego lo pasamos a array)
        List<Vector3> puntos = new List<Vector3>();

        //Añadimos los puntos a la lista
        for (int i = 0; i < newLine.positionCount; i++)
        {
            puntos.Add(newLine.GetPosition(i));
        }

        newLine.SetPositions(puntos.ToArray());
    }

    public Vector3 GetCenter()
    {
        Vector3 positions = new Vector3();
        //Recorremos los puntos para hacer la media
        for (int i = 0; i < line.positionCount; i++)
        {    
            positions += line.GetPosition(i);
        }

        _centralPoint.x = positions.x / line.positionCount;
        _centralPoint.y = positions.y / line.positionCount;
        _centralPoint.z = 0;

        return _centralPoint;
    }

    public Vector3 GetMinPoint()
    {
        Vector3 _minPoint = new Vector3();
        Vector3[] points = new Vector3[line.positionCount];
        Mathf.Min(line.GetPositions(points));
        return _minPoint;
    }

    public Vector3 GetMaxPoint()
    {
        Vector3 _maxPoint = new Vector3();
        Vector3[] points = new Vector3[line.positionCount];
        Mathf.Max(line.GetPositions(points));
        return _maxPoint;
    }

    public void EraseDrawing()
    {
        List<Vector3> vacio = new List<Vector3>();
        line.SetPositions(vacio.ToArray());

        Debug.Log(transform.childCount);
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        Debug.Log(transform.childCount);
    }
}
