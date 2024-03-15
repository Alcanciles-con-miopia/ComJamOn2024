using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Parameters

    [SerializeField] private GameObject _line;

    #endregion

    #region References
    private DrawingComponent _drawingComponent;
    public DrawingComponent DrawingComponent { get { return _drawingComponent; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _drawingComponent = _line.GetComponent<DrawingComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        //Al pulsar, se a�ade una l�nea
        if (Input.GetMouseButtonDown(0))
        {
            _drawingComponent.VariasLineas();
        }


        // CCAMBIAR ANTES DE COMMITEAR !!!!!!!

        //Cada vez que se pulsa, empieza o termina el trazo
        Debug.Log(GameManager.Instance != null);
        Debug.Log(_drawingComponent != null);
        Debug.Log(GameManager.Instance.CurrentState == GameManager.GameStates.GAME);
        if (Input.GetMouseButton(0))
        {
            if (GameManager.Instance != null
                && _drawingComponent != null
                && GameManager.Instance.CurrentState == GameManager.GameStates.GAME)
            {
                _drawingComponent.Paint(newPoint);
                Debug.Log("COJONES");
            }
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
            Debug.Log("Borra eso");
            // Existe el script GameManager puesto.
            if (GameManager.Instance != null)
                GameManager.Instance.QuitaDedo();
            
        }
    }
}
