using System.IO;
using UnityEngine;

public class SaveShapes : MonoBehaviour
{
    [SerializeField] DrawingComponent drawingComponent;

    public Shape RecordShape()
    {
        Shape shape = new Shape();


        /*
        shape.centroide = CalculateCentroide();
        shape.minDistance = CalculateMinDistance();
        shape.maxDistance = CalculateMaxDistance();
        shape.Perimetro = CalculatePerimetro();
        shape.orientacion = CalculateArea();
        shape.numeroPuntos = ;
        shape.densPuntos = CalculateDensidadPuntos();*/

        return shape;
    }
    #region Calculos de caracteristicas de las formas
    private Vector2 CalculateCentroide(Vector2[] points)
    {
        Vector2 centroide = new Vector2();

        return centroide;
    }

    private float CalculateMinDistance(Vector2[] points)
    {
        float minDistance = 0;

        return minDistance;
    }

    private float CalculateMaxDistance(Vector2[] points)
    {
        float maxDistance = 0;

        return maxDistance;
    }
    private float CalculatePerimetro(Vector2[] points)
    {
        float perimetro = 0;

        return perimetro;
    }
    private float CalculateArea(Vector2[] points)
    {
        float area = 0;

        return area;
    }
    private float CalculateOrientacion(Vector2[] points)
    {
        float orientacion = 0;

        return orientacion;
    }
    private int CalculateDensidadPuntos(Vector2[] points)
    {
        int densidadPuntos = 0;

        return densidadPuntos;
    }
    #endregion

    void WriteShape(Shape sp, StreamWriter sw)
    {
        //
        //Read the first line of text
        sw.WriteLine(sp.centroide);
        sw.WriteLine(sp.minDistance);
        sw.WriteLine(sp.maxDistance);
        sw.WriteLine(sp.Perimetro);
        sw.WriteLine(sp.orientacion);
        sw.WriteLine(sp.numeroPuntos);
        sw.WriteLine(sp.densPuntos);

        /*
        //Continue to read until you reach end of file
        while (line != null)
        {
            //write the line to console window
            Console.WriteLine(line);
            //Read the next line
            line = sr.ReadLine();
        }
        //close the file
        sr.Close();
        Console.ReadLine();*/
    }

    void WriteShapes(Shape[] sp)
    {
        StreamWriter sw = new StreamWriter("./Sample.txt");

        for (int i = 0; i < sp.Length; i++)
        {
            WriteShape(sp[i], sw);

        }

        sw.Close();
    }
}

public struct Shape
{
    public Vector2 centroide;
    public float minDistance;
    public float maxDistance;
    public float area;
    public float Perimetro;
    public float orientacion;
    public float numeroPuntos;
    public float densPuntos;
}