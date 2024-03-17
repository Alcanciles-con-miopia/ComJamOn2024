using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaComponent : MonoBehaviour
{
    public enum Acertijo
    {
        Teta,
        Amanecer,
        Piramides,
        Eclipse,
        Orbe,
        Flecha,
        Colgado,
        Pentagrama
    }

    [SerializeField] 
    private Sprite[] pistas;
    private SpriteRenderer _spriteRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (GameManager.Instance != null) { GameManager.Instance.RegisterPistaComponent(this);}
    }

    public void setPista(Acertijo index)
    {
        _spriteRenderer.sprite = pistas[(int)index];
    }
}
