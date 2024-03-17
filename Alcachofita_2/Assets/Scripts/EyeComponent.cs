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

    private Image _image;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();

        _image.color = _baseColor;
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void ChangeColor(float porcentaje)
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.GAME && _image != null && _shapeDetector != null)
        {
            //Bad Ending
            if (porcentaje > _shapeDetector.guessPercent)
            {
                Debug.Log("Porcentaje ojete: " + porcentaje);
                _image.color = _goodPercentage;
            }
            //Medium Ending
            else if (porcentaje > (_shapeDetector.guessPercent / 2))
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
    }

    IEnumerator ColorBase()
    {
        // Pausa de 2 segundos
        yield return new WaitForSeconds(2);
        _image.color = _baseColor;
    }
}
