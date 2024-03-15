using System;
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

        Debug.Log("Cant lineas: " + cantLineas);

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

        /* Vector3[] arrayfuses = new Vector3[cositas];
         fuseArrays(punteles, arrayfuses);

         for (int i = 0; i < arrayfuses.Length; i++)
         {
             Debug.Log(arrayfuses[i]);
         }
        */

        // True si esta en cositio
        if (_shapeDetector != null) 
            _shapeDetector.shapeDetected(fuseArrays(punteles));
    }

    /// <summary>
    /// Fusiona un array de arrays en uno solo
    /// </summary>
    /// <param name="punteles"> Array de arrays a fusionar</param>
    /// <returns></returns>
    private void fuseArrays(Vector3[][] punteles, Vector3[] arrayfuses)
    {


        // Vector de puntos mezclados

        Debug.Log("Elementos x: " + punteles.Length + "elementos y: " + punteles[0].Length);

        for (int i = 0; i < punteles.Length; i++)
        {
            for (int j = 0; j < punteles[i].Length; j++)
            {

                // Debug.Log("Puntitos pochos: " + punteles[i][j]);
                arrayfuses[i] = punteles[i][j];
                Debug.Log("Puntitos: " + arrayfuses[i]);
            }
        }

        Debug.Log(punteles.Length);

        // arrayfuses = puntitos;
    }
}
