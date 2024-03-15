using UnityEngine;
using UnityEngine.UI;

public class VignetteComponent : MonoBehaviour
{
    public Image _image;
    private float _increaseFactor;

    void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterVignette(this);

        _image = GetComponent<Image>();
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        _increaseFactor = 0.25f;
    }

    public void ChangeIntensity()
    {
        if (_image.color.a < 1)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _increaseFactor);
            Debug.Log(_increaseFactor);
            Debug.Log(_image.color.a);
            _increaseFactor += 0.25f;

            GetComponentInParent<ShakeComponent>();
        }
    }

}
