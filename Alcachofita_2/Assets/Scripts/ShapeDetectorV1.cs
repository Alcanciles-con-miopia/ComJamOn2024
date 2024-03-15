using UnityEngine;



public class ShapeDetectorV1 : MonoBehaviour
{
    // Cantidad de puntos entre este parametro son los puntos a comprobar.
    [SerializeField] private int checkResolution = 1;
    // Porcentaje a partir del que un dibujo se da por bueno.
    [SerializeField] private float guessPercent = 0.7f;

    float cantDentro = 0;
    // Cantidad de puntos totales
    int cantPuntos;

    [SerializeField] GameObject shape;
    [SerializeField] DrawingComponent drawingComponent;

    /// <summary>
    /// Detecta si los puntos estan dentro de un collider y si lo estan aumenta en uno a puntos en rango, si el porcentaje de aciertos es superior al necesario se da por acertada el dibujo
    /// </summary>
    public void shapeDetected()
    {
        Vector3[][] punteles = drawingComponent.GetPositions();
        AdaptShape(punteles);
        cantDentro = 0;
        // Cantidad de puntos totales
        cantPuntos = punteles.Length;

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
                    // Suma uno cuando el punto esta dentro del collider
                    cantDentro++;
                }
            }
        }

        Debug.Log("Puntos en la forma: " + cantDentro);
        Debug.Log("Puntos totales: " + cantPuntos);
        Debug.Log("porcentaje de acertados: " + (cantDentro / cantPuntos));


       // return cantDentro / cantPuntos >= guessPercent;
    }

    private bool Raycast(Vector2 pos)
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
    public float PorcentajeAcierto()
    {
        return (cantDentro / cantPuntos) * 100;
    }
    void AdaptShape(Vector3[][] Punteles)
    {
        //spriteRenderer.bounds.center.x - spriteRenderer.bounds.extents.x //Limite izquierdo sprite
        //spriteRenderer.bounds.center.x + spriteRenderer.bounds.extents.x //Limite derecho spritez

        //spriteRenderer.bounds.center.y - spriteRenderer.bounds.extents.y //Limite izquierdo sprite
        //spriteRenderer.bounds.center.y + spriteRenderer.bounds.extents.y //Limite derecho spritez
        GameObject shapeInst = Instantiate(shape, drawingComponent.GetCenter(), Quaternion.identity) ;

        SpriteRenderer runaSPR = shape.GetComponent<SpriteRenderer>();


        shapeInst.transform.localScale = new Vector3(
            ((runaSPR.bounds.center.x + runaSPR.bounds.extents.x) - (runaSPR.bounds.center.x - runaSPR.bounds.extents.x))/drawingComponent.XSize(), 
            ((runaSPR.bounds.center.y + runaSPR.bounds.extents.y) - (runaSPR.bounds.center.y - runaSPR.bounds.extents.y))/drawingComponent.YSize(), 
            0);

        shapeInst.transform.position = drawingComponent.GetCenter(); 

    }

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterShapeDetector(this);
        }
    }
}
