using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class ShapeDetectorV1 : MonoBehaviour
{
    // Cantidad de puntos entre este parametro son los puntos a comprobar.
    [SerializeField] private int checkResolution = 1;
    // Porcentaje a partir del que un dibujo se da por bueno.
    [SerializeField] private float guessPercent = 0.7f;

    /// <summary>
    /// Detecta si los puntos estan dentro de un collider y si lo estan aumenta en uno a puntos en rango, si el porcentaje de aciertos es superior al necesario se da por acertada el dibujo
    /// </summary>
    /// <param name="punteles">Array de puntos de un dibujo</param>
    /// <returns></returns>
    public bool shapeDetected(Vector3[] punteles)
    {
        int cantDentro = 0;

        //Comprueba una colision cada ciertos puntos
        for (int i = 0; i < punteles.Length; i += checkResolution)
        {
            RaycastHit2D hit = Physics2D.Raycast(punteles[i], Vector2.zero);

            //Colision en general, habra que comprobar con el collider concreto mediante layers (?)
            if (hit.collider != null)
            {
                Debug.Log("AYVAAAA");
                cantDentro++;
            }
        }



        return cantDentro / punteles.Length >= guessPercent;
    }
}
