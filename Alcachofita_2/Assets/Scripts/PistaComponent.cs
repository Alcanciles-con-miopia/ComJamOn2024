using UnityEngine;
using UnityEngine.UI;

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
    private Image _imageRenderer;

    private void Awake()
    {
        _imageRenderer = GetComponent<Image>();
        if (GameManager.Instance != null) { GameManager.Instance.RegisterPistaComponent(this); }
    }

    public void setPista(Acertijo index)
    {
        if (_imageRenderer == null)
        {
            _imageRenderer = GetComponent<Image>();
        }
        _imageRenderer.sprite = pistas[(int)index];
        _imageRenderer.SetNativeSize();
    }
}
