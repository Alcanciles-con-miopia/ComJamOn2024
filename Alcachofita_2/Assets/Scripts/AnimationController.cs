using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private string _animationState = "AnimState";

    void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null ||
            _animator.enabled == false)
        {
            enabled = false;
        }
    }


    public void OnButtonClick()
    {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameManager.GameStates.GAME)
        {
            if (_animator.GetInteger(_animationState) == 0)
            {
                _animator.SetInteger(_animationState, 1);
            }
            else if (_animator.GetInteger(_animationState) == 1)
            {
                _animator.SetInteger(_animationState, 0);
            }
        }
    }
    void Update()
    {

    }

}
