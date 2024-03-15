using UnityEngine;

public class DrawManager : MonoBehaviour
{

    [SerializeField] DrawingComponent _drawingComponent;


    /// <summary>
    /// Metodo para juntar arrays de puntos y comprobar si se ha completado correctamente la runa
    /// </summary>
    /// <param name="punteles"> cantidad indeterminada de arrays de puntos Vectores3</param>
    public void EndDraw()
    {

       

        /* Vector3[] arrayfuses = new Vector3[cositas];
         fuseArrays(punteles, arrayfuses);

         for (int i = 0; i < arrayfuses.Length; i++)
         {
             Debug.Log(arrayfuses[i]);
         }
        */

        // True si esta en cositio
        if (_shapeDetector != null)
            _shapeDetector.shapeDetected(punteles);
    }

}
