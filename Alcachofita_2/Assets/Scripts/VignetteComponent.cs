using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Unity.VisualScripting;

public class VignetteComponent : MonoBehaviour
{
    public RawImage _image;
    private float _increaseFactor = 51f;

    void Start()
    {
        _image = GetComponent<RawImage>();
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
    }


    public void ChangeIntensity()
    {
        if(_image.color.a < 255)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _increaseFactor);
            Debug.Log(_increaseFactor);
            Debug.Log(_image.color.a);
            //_increaseFactor += 51f;
        }
    }

}
