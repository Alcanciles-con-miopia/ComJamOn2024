using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeComponent : MonoBehaviour
{
    #region references
    private Animator _oscurecedorAnimator;
    #endregion

    private void Start()
    {
        _oscurecedorAnimator = GetComponent<Animator>();

        if (GameManager.Instance != null)
            GameManager.Instance.RegisterOscurecedor(this);
    }

    public void Transicion()
    {
        _oscurecedorAnimator.SetTrigger("Fadea");
        Debug.Log("Transiciono");
    }
}