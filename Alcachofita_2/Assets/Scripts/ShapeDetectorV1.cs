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
    public bool shapeDetected(Vector3[][] punteles)
    {
        float cantDentro = 0;
        // Cantidad de puntos totales
        int cantPuntos = punteles.Length;
        for (int i = 0; i < punteles.Length; i++)
        {
            cantPuntos += punteles[i].Length;
        }

        //Comprueba una colision cada ciertos puntos
        for (int i = 0; i < punteles.Length; i++)
        {
            for (int j = 0; j < punteles[i].Length; j += checkResolution)
            {
                //Debug.Log(punteles[i]);
                if (Raycast(punteles[i][j]))
                {
                    cantDentro++;
                }
            }
        }

        Debug.Log("Puntos en la forma: " + cantDentro);
        Debug.Log("Puntos totales: " + cantPuntos);
        Debug.Log("porcentaje de acertados: " + (cantDentro / cantPuntos));


        return cantDentro / cantPuntos >= guessPercent;
    }

    bool Raycast(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        //Colision en general, habra que comprobar con el collider concreto mediante layers (?)
        if (hit.collider != null)
        {
            //Debug.Log(pos);

            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(pos, forward, Color.green);
            return true;
        }
        Debug.Log("not collidea");
        return false;
    }
}
