using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Unity.VisualScripting;

public class VignetteComponent : MonoBehaviour
{
    [SerializeField] private Image image;
    public Color imageColor;

    void Start()
    {
        imageColor = image.color;
    }

    private void Update()
    {
        imageColor.a++;
        Debug.Log("lalineasilalineano");
    }
    public void ChangeIntensity()
    {
        imageColor.a ++;
        Debug.Log("lalineasilalineano");
    }
}
