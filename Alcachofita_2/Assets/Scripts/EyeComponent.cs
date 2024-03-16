using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EyeComponent : MonoBehaviour
{
    #region Parameters

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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.GAME)
        {
            //Bad Ending
            if (_shapeDetector.guessPercent <= 50)
            {
                _image.color = _badPercentage;
            }
            //Medium Ending
            else if (_shapeDetector.guessPercent > 50 && _shapeDetector.guessPercent <= 75)
            {
                _image.color = _mediumPercentage;
            }
            //Good Ending
            else if (_shapeDetector.guessPercent > 75)
            {
                _image.color = _goodPercentage;
            }
        }


    }
}
