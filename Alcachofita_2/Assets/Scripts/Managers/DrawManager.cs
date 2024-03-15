using UnityEngine;

public class DrawManager : MonoBehaviour
{

    [SerializeField] DrawingComponent _drawingComponent;
    [SerializeField] ShapeDetectorV1 _shapeDetector;


    /// <summary>
    /// Metodo para juntar arrays de puntos y comprobar si se ha completado correctamente la runa
    /// </summary>
    /// <param name="punteles"> cantidad indeterminada de arrays de puntos Vectores3</param>
    public void EndDraw()
    {

        int cantLineas = _drawingComponent.gameObject.GetComponent<Transform>().childCount;

        Vector3[][] punteles = new Vector3[cantLineas][];


        for (int i = 0; i < cantLineas; i++)
        {
            LineRenderer linerendrs = _drawingComponent.gameObject.GetComponent<Transform>().GetChild(i).GetComponent<LineRenderer>();
            punteles[i] = new Vector3[linerendrs.positionCount];
            for (int j = 0; j < punteles[i].Length; j++)
            {
                linerendrs.GetPositions(punteles[i]);
            }

        }

        // True si esta en cositio
        _shapeDetector.shapeDetected(fuseArrays(punteles));
    }

    /// <summary>
    /// Fusiona un array de arrays en uno solo
    /// </summary>
    /// <param name="punteles"> Array de arrays a fusionar</param>
    /// <returns></returns>
    private Vector3[] fuseArrays(Vector3[][] punteles)
    {
        // Cantidad de puntos totales
        int cositas = punteles.Length;
        for (int i = 0; i < punteles.Length; i++)
        {
            cositas += punteles[i].Length;
        }

        // Vector de puntos mezclados
        Vector3[] puntitos = new Vector3[cositas];

        for (int i = 0; i < punteles.Length; i++)
        {
            for (int j = 0; j < punteles[i].Length; j++)
            {

                puntitos[i] = punteles[i][j];
            }
        }

        return puntitos;
    }
}
