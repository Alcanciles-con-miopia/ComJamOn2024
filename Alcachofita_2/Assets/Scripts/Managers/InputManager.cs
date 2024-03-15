using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Parameters

    [SerializeField] private LineRenderer _line;

    #endregion

    #region References

    private DrawingComponent _drawingComponent;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (_line != null) _drawingComponent = _line.GetComponent<DrawingComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        ////Al pulsar, se añade una línea
        //if (Input.GetMouseButtonDown(0))
        //{
        //    _drawingComponent.StartLine(newPoint);
        //}
        ////Al dejar de pulsar, se deja de dibujar
        //if (Input.GetMouseButtonUp(0))
        //{
        //    _drawingComponent.FinishLine();
        //}

        //Cada vez que se pulsa, empieza o termina el trazo
        if (Input.GetMouseButton(0))
        {
            if (GameManager.Instance != null 
                && _drawingComponent != null
                && GameManager.Instance.CurrentState == GameManager.GameStates.GAME)
                _drawingComponent.Paint(newPoint);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("boton derecho");
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("boton medio");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.QuitaDedo();
            Debug.Log("Borra eso");
        }
    }
}
