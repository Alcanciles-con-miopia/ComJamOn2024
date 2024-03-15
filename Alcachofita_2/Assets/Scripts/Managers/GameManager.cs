using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public enum GameStates { MAINMENU, GAME, END };
    const int NUM_DEDOS = 5;

    // variable a actualizar cada vez que se corte un dedo
    public bool ISDEAD = false;

    #region references
    // dedos por orden de corte.
    private GameObject PULGAR,
                       INDICE,
                       CORAZON,
                       ANULAR,
                       MENIQUE;

    // ARRAY DE DEDALOS
    // inicialmente se tienen 5 dedos.
    [SerializeField]
    public GameObject[] dedos = new GameObject[NUM_DEDOS];

    [SerializeField] private GameObject mano;

    // UIManager
    private UIManager _UIManager;

    // VignetteComponent
    private VignetteComponent _VignetteComponent;
    private RagdollComponent _ragdollComponent;

    #endregion
    
    #region properties
    // GAMEMANAGER
    private static GameManager _gameManager;
    public static GameManager Instance { get { return _gameManager; } }

    // INPUT
    private InputManager _input;
    public InputManager Input { get { return _input; } }

    // ESTADOS
    private GameStates _currentGameState;
    public GameStates CurrentState { get { return _currentGameState; } }

    private GameStates _nextGameState;
    public GameStates NextState { get { return _nextGameState; } }

    // DEDOS
    // Es el próximo dedo que tiene que cortarse.
    private int _nextDedo;
    public int NextDedo { get { return _nextDedo; } }

    // PAGINAS
    // no. de pagina actual
    private int _currentPage;
    public int CurrentPage { get { return _currentPage; } }
    #endregion

    #region METODOS DE ESTADOS
    // ---- requestStateChange ----
    // establece cual es el estado siguiente al que ir
    public void requestSateChange(GameStates newState)
    {
        // guarda el estado correspondiente en next
        _nextGameState = newState;
    }

    // ---- onStateEnter ----
    // decide que hacer en cada estado
    public void onStateEnter(GameStates newState)
    {
        switch (newState)
        {
            // ---- MAIN MENU ----
            case GameStates.MAINMENU:

                break;

            // ---- GAME ----
            case GameStates.GAME:

                break;

            // ---- END ----
            case GameStates.END:

                break;
        }

        // guarda el estado correspondiente en current
        _currentGameState = newState;

        if (_UIManager != null) { _UIManager.SetMenu(newState); }

        Debug.Log("Nosss encontramoS en el eStado: " + _currentGameState);
    }

    // ---- updateState ----
    // sobretodo para input y ui y cosas asi ?????
    public void updateState(GameStates state)
    {
        switch (state)
        {
            // ---- MAIN MENU ----
            case GameStates.MAINMENU:

                break;

            // ---- GAME ----
            case GameStates.GAME:

                break;

            // ---- END ----
            case GameStates.END:

                break;
        }
    }
    #endregion

    #region METODOS DE VIGNETTE
    public void RegisterVignette(VignetteComponent vignette)
    {
        _VignetteComponent = vignette;
    }
    #endregion

    #region METODOS DE RAGDOLL

    public void RegisterRagdoll(RagdollComponent ragdoll)
    {
        _ragdollComponent = ragdoll;
    }

    #endregion

    #region METODOS DE DEDOS
    // ---- InicializaDedos ----
    // settea cada indice del array con su dedo correspondiente
    // en orden de cortado
    private void InicializaDedos()
    {
        _nextDedo = 0; // El próximo dedo a cortar es el dedos[0]

        Debug.Log(dedos.Length);

        // Set active todos los dedalos.
        for (int i = 0; i < dedos.Length; i++)
        {
            dedos[i].SetActive(true);
        }
    }

    // ---- QuitaDedo ----
    // modifica el array sin el ultimo dedo a cortar
    // borra del array el nextDedo que debe de actualizarse siempre
    public void QuitaDedo()
    {
        // Siempre y cuando el índice sea menor que dedos.Length...
        if (_nextDedo < dedos.Length)
        {
            // Se desactiva el dedo actual (de momento, luego hará lo del ragdoll y al salir de pantalla DESACTIVAR).
            //dedos[_nextDedo].SetActive(false);

            _VignetteComponent.ChangeIntensity();
            dedos[NextDedo].GetComponent<RagdollComponent>().SeparaDedo();
            mano.GetComponent<ShakeComponent>().ShakeSpeedChanger(3);

            // Siguiente dedo a cortar.
            _nextDedo++;
        }
    }

    // ---- isDead ----
    // hace ISDEAD true si ya no quedan dedos
    public void isDead()
    {
        if (_nextDedo >= 5)
        {
            ISDEAD = true;
        }
        else { ISDEAD = false; }
    }
    #endregion

    #region METODOS DE PAGINAS
    public void RegisterUIManager(UIManager uiManager)
    {
        _UIManager = uiManager;
    }

    public void SetPage(int page) { _currentPage = page; }

    public void NextPage()
    {
        //if (drawcomponent.comprobar etc...)
        _currentPage++;

        if (_currentPage >= 3)
        {
            requestSateChange(GameStates.END);
        }
    }

    public void LastPage() { _currentPage--; }
    #endregion

    private void Awake()
    {
        // si ya existe instancia del gamemanager se destruye
        if (_gameManager != null) { Destroy(this); }

        // en otro caso la asigna
        else
        {
            _gameManager = this;

            // si se guarda info en el gameManager y se ha de recargar
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Se inicializa los dedos.
        InicializaDedos();

        // para cuando exista el input 
        _input = GetComponent<InputManager>();

        // inicializacion del numero de pagina actual
        _currentPage = 0;

        // inducimos primer onEnter con valor dummy del estado
        _currentGameState = GameStates.END;
        _nextGameState = GameStates.GAME; // valor real inicial
    }

    // Update is called once per frame
    void Update()
    {
        // si se debe cambiar de estado (next y current difieren)
        if (_nextGameState != _currentGameState)
        {
            // se cambia
            onStateEnter(_nextGameState);
        }

        // se actualiza el estado en el que se este
        updateState(_currentGameState);
    }
}