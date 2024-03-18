using UnityEngine;



public class ShapeDetectorV1 : MonoBehaviour
{
    // Cantidad de puntos entre este parametro son los puntos a comprobar.
    [SerializeField] private int checkResolution = 1;
    // Porcentaje a partir del que un dibujo se da por bueno.
    [SerializeField] public float guessPercent = 0.7f;
    [SerializeField] LayerMask layerMask;

    float cantDentro = 0;
    // Cantidad de puntos totales
    int cantPuntos;
    Vector3[][] punteles;

    // Runa a comprobar
    [SerializeField] GameObject shape;
    [SerializeField] DrawingComponent drawingComponent;
    GameObject shapeInst;

    // Para ampliar el collider si hace falta
    [SerializeField] float magicosidadDeLaEscala = 1;

    /// <summary>
    /// Detecta si los puntos estan dentro de un collider y si lo estan aumenta en uno a puntos en rango, si el porcentaje de aciertos es superior al necesario se da por acertada el dibujo
    /// </summary>
    public bool shapeDetected()
    {
        cantDentro = 0; // Cantidad de puntos en la forma        
                        // Consigue los puntos a evaluar
        punteles = drawingComponent.GetPositions();
        CantidadPuntosDibujados(); //Cantidad puntos dibujados

        if (cantPuntos > 0)
        {



            // Adapta el collider
            // AdaptShape();

            //shapeInst.transform.parent = transform;
            //shapeInst.transform.position = transform.position;



            CheckCollisions();

            /*
            Debug.Log("Puntos en la forma: " + cantDentro);
            Debug.Log("Puntos totales: " + cantPuntos);*/
            //Debug.Log("porcentaje de acertados: " + (cantDentro / cantPuntos));

            drawingComponent.EraseDrawing();

            GameManager.Instance.ChangeEyeColor(cantDentro / cantPuntos, guessPercent);

            return cantDentro / cantPuntos >= guessPercent;
        }
        return false;
    }

    public int CantidadPuntosDibujados()
    {
        cantPuntos = punteles.Length; // Cantidad de puntos totales

        //Cuenta todos los puntos en el dibujo
        for (int i = 0; i < punteles.Length; i++)
        {
            cantPuntos += punteles[i].Length;
        }

        //Debug.Log("Punteles:" + cantPuntos);

        return cantPuntos;
    }

    void CheckCollisions()
    {
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
                    //Debug.Log("TU PUTA MADRE" + cantDentro);
                }
            }
        }
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
        // Debug.Log("not collidea");
        return false;
    }
    public float PorcentajeAcierto()
    {
        return (cantDentro / cantPuntos) * 100;
    }
    void AdaptShape()
    {
        //spriteRenderer.bounds.center.x - spriteRenderer.bounds.extents.x //Limite izquierdo sprite
        //spriteRenderer.bounds.center.x + spriteRenderer.bounds.extents.x //Limite derecho spritez

        //spriteRenderer.bounds.center.y - spriteRenderer.bounds.extents.y //Limite izquierdo sprite
        //spriteRenderer.bounds.center.y + spriteRenderer.bounds.extents.y //Limite derecho spritez
        GameObject shapeInst = Instantiate(shape, drawingComponent.GetCenter(), Quaternion.identity);


        shapeInst.transform.parent = transform;

        shapeInst.transform.position = drawingComponent.GetCenter();

        SpriteRenderer runaSPR = shapeInst.GetComponent<SpriteRenderer>();

        // shapeInst.GetComponent<PolygonCollider2D>().

        float ancho = (runaSPR.bounds.center.x + runaSPR.bounds.extents.x) - (runaSPR.bounds.center.x - runaSPR.bounds.extents.x);
        float alto = (runaSPR.bounds.center.y + runaSPR.bounds.extents.y) - (runaSPR.bounds.center.y - runaSPR.bounds.extents.y);


        //Debug.Log("Ancho sprite: " + ancho);
        Vector3 scale = new Vector3(
            (drawingComponent.XSize() - (ancho - magicosidadDeLaEscala)) / ancho,
            (drawingComponent.XSize() - (ancho - magicosidadDeLaEscala)) / ancho,
            0);
        //(drawingComponent.YSize() - (alto - magicosidadDeLaEscala)) / alto);
        //ancho = (runaSPR.bounds.center.x + runaSPR.bounds.extents.x) - (runaSPR.bounds.center.x - runaSPR.bounds.extents.x);

        //Debug.Log("Ancho dibujo: " + drawingComponent.XSize());
        #region modificacion de puntos
        /*
        //shapeInst.transform.localScale = scale;

        Vector2[] colliderPoints = shapeInst.GetComponent<PolygonCollider2D>().points;

        // Creamos un nuevo array para almacenar los puntos escalados
        Vector2[] puntosEscalados = new Vector2[colliderPoints.Length];

        // Escalamos cada punto
        for (int i = 0; i < colliderPoints.Length; i++)
        {
            puntosEscalados[i] = new Vector2(
                    (drawingComponent.XSize() / colliderPoints[i].x),
                    (drawingComponent.XSize()/ colliderPoints[i].x)
                );
        }
        */
        // Asignamos los puntos escalados al collider
        #endregion


        shapeInst.transform.localScale = scale;
        //shapeInst.GetComponent<PolygonCollider2D>().points = puntosEscalados;

    }

    /// <summary>
    /// Cambia la runa a comprobar a la que le pasas por parametro
    /// </summary>
    /// <param name="newRune"></param>
    public void ChangeRune(ShapeSO newRune)
    {

        if (newRune == null)
        {
            Debug.Log("PUTAMENTE NULO");
            return;
        }
        //Debug.Log(newRune);
        shape = newRune.runa;
        guessPercent = newRune.probabilidadExito;
        // Elimina hijos
        Destroy(shapeInst);
        // Crea el collider
        shapeInst = Instantiate(shape);
        /*
        if (GameManager.Instance != null && shape != null)
        {
            if (shape.GetComponent<SpriteRenderer>() != null)
            {
                if (shape.GetComponent<SpriteRenderer>().sprite != null)
                {
                    GameManager.Instance.ChangeRuneSprite(shape.GetComponent<SpriteRenderer>().sprite);

                }
            }
        }*/

        //Debug.Log("AAAAAAaa");
    }

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterShapeDetector(this);
        }
    }
}
