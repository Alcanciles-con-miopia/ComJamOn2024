using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawingComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] float _minDistance = 0.1f;
    [SerializeField] Material _material;
    [SerializeField] float _startWidth = 0.5f;
    [SerializeField] float _endWidth = 0.5f;
    [SerializeField] ShapeDetectorV1 _shapeDetector;

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
        if (GameManager.Instance != null) { GameManager.Instance.RegisterDrawingComponent(this); }
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

        Vector3 centerMax = Vector3.zero;

        Vector3[][] punteles = GetPositions();

        for (int i = 0; i < punteles.Length; i++)
        {
            for (int j = 0; j < punteles[i].Length; j++)
            {
                if (centerMax.x < punteles[i][j].x)
                    centerMax.x = punteles[i][j].x;
                if (centerMax.y < punteles[i][j].y)
                    centerMax.y = punteles[i][j].y;
            }
        }

        Vector3 centerMin = centerMax;
        for (int i = 0; i < punteles.Length; i++)
        {
            for (int j = 0; j < punteles[i].Length; j++)
            {
                if (centerMin.x > punteles[i][j].x)
                    centerMin.x = punteles[i][j].x;
                if (centerMin.y > punteles[i][j].y)
                    centerMin.y = punteles[i][j].y;
            }
        }
        return new Vector3(
            ((centerMax.x - centerMin.x) / 2) + centerMin.x,
            ((centerMax.y - centerMin.y) / 2) + centerMin.y,
            0);
    }

    public float XSize()
    {
        Vector3[][] punteles = GetPositions();


        Vector3 _minPoint = punteles[0][0];
        Vector3 _maxPoint = punteles[0][0];
        //float size;

        for (int i = 0; i < punteles.Length; i++)
        {
            for (int j = 0; j < punteles[i].Length; j++)
            {
                if (punteles[i][j].x < _minPoint.x)
                    _minPoint = punteles[i][j];
                if (punteles[i][j].x > _maxPoint.x)
                    _maxPoint = punteles[i][j];
            }
        }

        return _maxPoint.x - _minPoint.x;
    }

    public float YSize()
    {
        Vector3[][] punteles = GetPositions();

        Vector3 _minPoint = punteles[0][0];
        Vector3 _maxPoint = punteles[0][0];
        //float size;

        for (int i = 0; i < punteles.Length; i++)
        {
            for (int j = 0; j < punteles[i].Length; j++)
            {
                if (punteles[i][j].y < _minPoint.y)
                    _minPoint = punteles[i][j];
                if (punteles[i][j].y > _maxPoint.y)
                    _maxPoint = punteles[i][j];
            }
        }

        return _maxPoint.y - _minPoint.y;
    }

    public void EraseDrawing()
    {
        List<Vector3> vacio = new List<Vector3>();
        if (line != null) line.SetPositions(vacio.ToArray());


        //Debug.Log(transform.childCount);

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


        // Debug.Log(transform.childCount);

    }

    /// <summary>
    /// 
    /// </summary>
    public Vector3[][] GetPositions()
    {
        int cantLineas = gameObject.GetComponent<Transform>().childCount;

        //Debug.Log("Cant lineas: " + cantLineas);

        Vector3[][] punteles = new Vector3[cantLineas][];

        //Debug.Log(cantLineas);
        for (int i = 0; i < cantLineas; i++)
        {
            LineRenderer linerendrs = gameObject.GetComponent<Transform>().GetChild(i).GetComponent<LineRenderer>();
            punteles[i] = new Vector3[linerendrs.positionCount];
            for (int j = 0; j < punteles[i].Length; j++)
            {
                linerendrs.GetPositions(punteles[i]);
            }

        }
        return punteles;
    }
}
