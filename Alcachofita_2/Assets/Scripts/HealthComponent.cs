using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    #region parameters
    // error es cuando hay un error en el dibujo.
    private bool error = false;
    #endregion

    public int QuitaDedo(int dedo)
    {
        dedo = 5;

        if (error) dedo--;
        return dedo;
    }
}
