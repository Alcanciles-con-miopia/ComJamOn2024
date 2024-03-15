using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New Shape", menuName = "Category/Shape", order = 1)]
public class ShapeSO : ScriptableObject
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