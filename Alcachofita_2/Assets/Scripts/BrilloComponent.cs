using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class BrilloComponent : MonoBehaviour
{
    public SpriteRenderer trapo;

    void OnMouseOver()
    {
        //Debug.Log("pis");
        if (trapo.color.a < 1)
        {
            trapo.color = new Color(trapo.color.r, trapo.color.g, trapo.color.b, 1);
        }
    }

    void OnMouseExit()
    {
        //Debug.Log("caca");
        if (trapo != null)
        {
            trapo.color = new Color(trapo.color.r, trapo.color.g, trapo.color.b, 0);
        }
    }

    void Start()
    {
        trapo = GetComponent<SpriteRenderer>();
        trapo.color = new Color(trapo.color.r, trapo.color.g, trapo.color.b, 0);
    }
}
