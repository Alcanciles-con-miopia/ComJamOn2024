using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaComponent : MonoBehaviour
{
    [SerializeField] 
    private Sprite[] pistas;
    private SpriteRenderer _spriteRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setPista(int index)
    {
        _spriteRenderer.sprite = pistas[index];
    }
}
