using UnityEngine;
using UnityEngine.UI;

public class VignetteComponent : MonoBehaviour
{
    public Image _image;
    private float _increaseFactor;

    [SerializeField] private AudioClip _arrancaDedo;
    [SerializeField] private AudioClip _pasaPagina;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

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
            _increaseFactor += 0.20f;

            _audioSource.clip = _arrancaDedo;
            _audioSource.Play();

            _audioSource.clip = _pasaPagina;
            _audioSource.Play();
        }
    }

    public void ResetIntensity()
    {
        if (_image != null)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        }
    }
}
