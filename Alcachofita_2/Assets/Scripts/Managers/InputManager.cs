using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Parameters
    [SerializeField] private GameObject _line;
    [SerializeField] private AudioClip _escribeSound;
    private Vector3 mousePos = Vector3.zero;
    private AudioSource _audioSource;

    private int LEFT_OFFSET = Screen.width / 2 + Screen.width / 100;
    private int RIGHT_OFFSET = Screen.width / 5;
    private int UP_OFFSET = Screen.height / 5 - Screen.width / 100;
    private int DOWN_OFFSET = Screen.height / 6;
    #endregion

    #region References
    private DrawingComponent _drawingComponent;
    public DrawingComponent DrawingComponent { get { return _drawingComponent; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _drawingComponent = _line.GetComponent<DrawingComponent>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _escribeSound;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameManager.GameStates.GAME)
        {
            //Al pulsar, se a�ade una l�nea
            if (Input.GetMouseButtonDown(0))
            {
                mousePos = Input.mousePosition;
                if (_drawingComponent != null
                    && GameManager.Instance.CurrentState == GameManager.GameStates.GAME
                    && mousePos.x > LEFT_OFFSET
                    && mousePos.x < Screen.width - RIGHT_OFFSET
                    && mousePos.y < Screen.height - UP_OFFSET
                    && mousePos.y > DOWN_OFFSET)
                {
                    //_drawingComponent.Paint(newPoint);
                    _drawingComponent.VariasLineas();
                    //Debug.Log("COJONES");
                }
            }


            // CCAMBIAR ANTES DE COMMITEAR !!!!!!!

            //Cada vez que se pulsa, empieza o termina el trazo
            if (Input.GetMouseButton(0))
            {
                mousePos = Input.mousePosition;
                if (_drawingComponent != null
                    && mousePos.x > LEFT_OFFSET
                    && mousePos.x < Screen.width - RIGHT_OFFSET
                    && mousePos.y < Screen.height - UP_OFFSET
                    && mousePos.y > DOWN_OFFSET)
                {
                    if (_drawingComponent != null) _drawingComponent.Paint(newPoint);
                    if (_audioSource != null && !_audioSource.isPlaying) _audioSource.Play();

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
                //Debug.Log("Borra eso");
                // Existe el script GameManager puesto.
                if (GameManager.Instance != null)
                    GameManager.Instance.QuitaDedo();

            }

            if (Input.GetMouseButtonUp(0))
            {
                _audioSource.Stop();
            }
        }
    }
}
