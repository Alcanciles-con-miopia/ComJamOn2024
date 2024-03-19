using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class EyeComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _badPercentage;
    [SerializeField] private Color _mediumPercentage;
    [SerializeField] private Color _goodPercentage;

    [SerializeField] private ShapeDetectorV1 _shapeDetector;

    #endregion

    #region Reference

    private SpriteRenderer _image;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<SpriteRenderer>();

        _image.color = _baseColor;
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void ChangeColor(float porcentaje, float guessPercent)
    {
        Debug.Log("Porcentaje ojete: " + porcentaje);
        if (GameManager.Instance.CurrentState == GameManager.GameStates.GAME && _image != null && _shapeDetector != null)
        {
            //Bad Ending
            if (porcentaje > guessPercent)
            {
                _image.color = _goodPercentage;
            }
            //Medium Ending
            else if (porcentaje > (guessPercent / 2))
            {
                _image.color = _mediumPercentage;
            }
            else
            //Good Ending
            {
                _image.color = _badPercentage;
            }

            StartCoroutine(ColorBase());
        }
        else
        {
            _image.color = _baseColor;
        }
    }

    IEnumerator ColorBase()
    {
        // Pausa de 2 segundos
        yield return new WaitForSeconds(2);
        _image.color = _baseColor;
    }
}
