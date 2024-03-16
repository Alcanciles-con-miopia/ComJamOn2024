using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeComponent : MonoBehaviour
{
    #region Parameters

    [SerializeField] private Color _badPercentage;
    [SerializeField] private Color _mediumPercentage;
    [SerializeField] private Color _goodPercentage;

    [SerializeField] private ShapeDetectorV1 _shapeDetector;

    #endregion

    #region Reference

    private SpriteRenderer _spriteRenderer; 

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bad Ending
        if (_shapeDetector.guessPercent <= 50)
        {
            _spriteRenderer.color = _badPercentage;
        }
        //Medium Ending
        else if (_shapeDetector.guessPercent > 50 && _shapeDetector.guessPercent <= 75)
        {
            _spriteRenderer.color = _mediumPercentage;
        }
        //Good Ending
        else if (_shapeDetector.guessPercent > 75)
        {
            _spriteRenderer.color = _goodPercentage;
        }
    }
}
