using UnityEngine;

public class CursorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 10;
        transform.position = mousePosition;

        if (GetComponent<ShakeComponent>() != null )
        {
            transform.position = GetComponent<ShakeComponent>().Shake(mousePosition);
        }
    }
}
