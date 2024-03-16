using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField] GameObject _cursorImage;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        _cursorImage.transform.localScale = Input.mousePosition;
    }
}
