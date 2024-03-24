using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDetectorV2 : MonoBehaviour
{
    Vector3[][] punteles;
    int checkResolution = 1,
        cantDentro = 0,
        cantPuntos = 0;

    [SerializeField] public float maxGuessPercent = 0.7f;
    [SerializeField] public float minGuessPercent = 0.2f;
    public float percentaje;

    [SerializeField]
    ShapeSO shape;

    [SerializeField]
    DrawingComponent drawingComponent;
    ShapeColliderGenerator shapeCollider;

    private void Start()
    {
        shapeCollider = GetComponent<ShapeColliderGenerator>();
    }

    public bool CheckShape()
    {
        bool acceptShape = false;
        cantDentro = 0;
        cantPuntos = 0;

        punteles = drawingComponent.GetPositions();

        // Genera el collider
        //shapeCollider.GenerateShape(shape);

        CantidadPuntosDibujados();
        CheckCollisions();
        // Comprueba las cosas

        //Debug.Log("CanrDentro: " + cantDentro);
        //Debug.Log("CantPuntos: " + cantPuntos);

        percentaje = (float)cantDentro / (float)cantPuntos;
        //Debug.Log("Porcentaje: " + percentaje);

        acceptShape = percentaje > maxGuessPercent && percentaje > minGuessPercent;
        acceptShape = acceptShape && shapeCollider.AllPointsCollided(punteles);

        Debug.Log(acceptShape);
        drawingComponent.EraseDrawing();

        GameManager.Instance.ChangeEyeColor(percentaje, maxGuessPercent, minGuessPercent);


        // Return de la comprobacion
        return acceptShape;
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

    public void ChangeRune(ShapeSO newRune)
    {

        if (newRune == null)
        {
            Debug.Log("PUTAMENTE NULO");
            return;
        }
        maxGuessPercent = newRune.probabilidadExito;
        //Debug.Log(newRune);
        // Crea el collider
        shapeCollider.GenerateShape(newRune);
    }

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterShapeDetector2(this);
        }
    }
}
