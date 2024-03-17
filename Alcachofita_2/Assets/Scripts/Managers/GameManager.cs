using UnityEngine;
using static Unity.Collections.Unicode;

public class GameManager : MonoBehaviour
{
    public enum GameStates { INTRO, LORE, MAINMENU, GAME, END, CREDITS };
    const int NUM_DEDOS = 5;

    // variable a actualizar cada vez que se corte un dedo
    public bool ISDEAD;
    public bool ISWIN;

    #region references
    // ARRAY DE DEDALOS
    // inicialmente se tienen 5 dedos.
    [SerializeField]
    private GameObject[] dedos = new GameObject[NUM_DEDOS];
    [SerializeField]
    private GameObject mano;

    [SerializeField]
    private GameObject cursor;

    private UIManager _UIManager;
    [SerializeField]
    private ShapeDetectorV1 _ShapeDetector;
    private DrawingComponent _drawingComp;
    [SerializeField]
    private PistaComponent _pistaComp;
    [SerializeField]
    private BGMComponent _bGMComponent;

    private VignetteComponent _VignetteComponent;
    private RagdollComponent _ragdollComponent;
    private FadeComponent _fadeComponent;

    // Array de runas
    [SerializeField]
    private ShapeSO[] runas;
    [SerializeField]
    private int[] runasUsadas;

    [SerializeField]
    EyeComponent _eyeComponent;

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
    // Es el pr�ximo dedo que tiene que cortarse.
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
        if (_drawingComp != null) { _drawingComp.EraseDrawing(); }
        if (_input != null && _input.aSource != null) { _input.aSource.Stop(); }
        _nextGameState = newState;
    }

    // ---- onStateEnter ----
    // decide que hacer en cada estado
    public void onStateEnter(GameStates newState)
    {
        Cursor.visible = false;
        switch (newState)
        {
            // ---- INTRO ----
            case GameStates.INTRO:
                cursor.GetComponent<SpriteRenderer>().enabled = false;
                break;

            // ---- LORE ----
            case GameStates.LORE:
                Cursor.visible = true;
                cursor.GetComponent<SpriteRenderer>().enabled = false;
            break;

            // ---- MAIN MENU ----
            case GameStates.MAINMENU:
                cursor.GetComponent<SpriteRenderer>().enabled = true;
                break;

            // ---- GAME ----
            case GameStates.GAME:
                cursor.GetComponent<SpriteRenderer>().enabled = true;

                // Poner todas las runas a usar como todas las runas
                runasUsadas = new int[runas.Length];
                for (int i = 0; i < runasUsadas.Length; i++)
                {
                    runasUsadas[i] = -1;
                }

                // cambia la runa a comprobar
                int nextRune = UsarRuna();
                if (_UIManager != null) _UIManager.ChangeAcertijoNumber(nextRune);
                if (_pistaComp != null) _pistaComp.setPista((PistaComponent.Acertijo)nextRune);
                if (runas.Length > 0 && _ShapeDetector != null && nextRune <= runas.Length)
                {
                    
                    Debug.Log(runas[nextRune]);
                    _ShapeDetector.ChangeRune(runas[nextRune]);
                }

                break;

            // ---- END ----
            case GameStates.END:
                Cursor.visible = true;
                cursor.GetComponent<SpriteRenderer>().enabled = false;

                if (_bGMComponent != null) _bGMComponent.PlayBGM(3);
                if (ISWIN) { if (_UIManager != null) _UIManager.SetWin(); }
                break;

            // ---- CREDITS ----
            case GameStates.CREDITS:
                cursor.GetComponent<SpriteRenderer>().enabled = false;
                break;
        }

        // guarda el estado correspondiente en current
        _currentGameState = newState;
        if (_VignetteComponent != null) _VignetteComponent.ResetIntensity();
        if (_UIManager != null) { _UIManager.SetMenu(newState); }
        if (_bGMComponent != null) _bGMComponent.PlayBGM((int)_currentGameState);

        Debug.Log("Nosss encontramoS en el eStado: " + _currentGameState);
    }

    // ---- updateState ----
    // sobretodo para input y ui y cosas asi ?????
    public void updateState(GameStates state)
    {
        switch (state)
        {
            // ---- INTRO ----
            case GameStates.INTRO:
                break;

            case GameStates.LORE:
            break;

            // ---- MAIN MENU ----
            case GameStates.MAINMENU:


                break;

            // ---- GAME ----
            case GameStates.GAME:

                break;

            // ---- END ----
            case GameStates.END:

                break;

            // ---- CREDITS ----
            case GameStates.CREDITS:
                break;
        }
    }
    #endregion

    #region METODOS DE OSCURECEDOR
    public void RegisterOscurecedor(FadeComponent fade)
    {
        _fadeComponent = fade;
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

    #region METODOS DE MUSICA

    public void RegisterBGM(BGMComponent bgm)
    {
        //_bGMComponent = bgm;
    }

    #endregion

    #region METODOS DE DEDOS
    // ---- InicializaDedos ----
    // settea cada indice del array con su dedo correspondiente
    // en orden de cortado
    private void InicializaDedos()
    {
        _nextDedo = 0; // El pr�ximo dedo a cortar es el dedos[0]

        //Debug.Log(dedos.Length);

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
        // Siempre y cuando el �ndice sea menor que dedos.Length...
        if (_nextDedo < dedos.Length)
        {
            // Se desactiva el dedo actual (de momento, luego har� lo del ragdoll y al salir de pantalla DESACTIVAR).
            //dedos[_nextDedo].SetActive(false);

            if (_VignetteComponent != null) _VignetteComponent.ChangeIntensity();
            dedos[NextDedo].GetComponent<RagdollComponent>().SeparaDedo();
            mano.GetComponent<ShakeComponent>().ShakeSpeedChanger(3);
            cursor.GetComponent<ShakeComponent>().ShakeSpeedChanger(3);

            // Siguiente dedo a cortar.
            _nextDedo++;
        }
        else
        {
            ISDEAD = true;
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

    public void RegisterShapeDetector(ShapeDetectorV1 shapeDetector)
    {
        _ShapeDetector = shapeDetector;
    }

    public void RegisterDrawingComponent(DrawingComponent drawComp)
    {
        _drawingComp = drawComp;
    }

    public void RegisterPistaComponent(PistaComponent pistaComp)
    {
        _pistaComp = pistaComp;
    }

    public void SetPage(int page) { _currentPage = page; }

    public void NextPage()
    {
        if (_ShapeDetector != null && _ShapeDetector.shapeDetected()) // si es valide
        {
            _currentPage++; // siguiente runa

            // aqu� habr�a que cambiar la pista de fondo

            // cambia la runa a comprobar
            int nextRune = UsarRuna();

            _ShapeDetector.ChangeRune(runas[nextRune]);

            _pistaComp.setPista((PistaComponent.Acertijo)nextRune);


            if (_currentPage >= 3) // si ya ha llegado al final
            {
                requestSateChange(GameStates.END);
                ISWIN = true; // gana ! gloria ! orbe catatonico
            }
        }
        else if (_ShapeDetector.CantidadPuntosDibujados() > 0) // si no es dibujo v�lide
        {
            QuitaDedo();
            isDead();
            if (_drawingComp != null) { _drawingComp.EraseDrawing(); }
        }

        _eyeComponent.ChangeColor(_ShapeDetector.PorcentajeAcierto());
    }
    int UsarRuna()
    {
        bool usable = true;
        int i = 0;

        int nextId = Random.Range(0, 5);

        while (i < runas.Length && usable)
        {
            if(nextId==runasUsadas[i])
            {
                usable = false;
                nextId = Random.Range(0, runas.Length);
            }
            i++;
        }

        i = 0;

        while (i < runas.Length && runasUsadas[i] != -1)
        {
            
            i++;
        }

        runasUsadas[i] = nextId;

        return nextId;
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
            //DontDestroyOnLoad(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (_bGMComponent != null) _bGMComponent.StopAll();
        if (SFXComponent.Instance != null) SFXComponent.Instance.StopAll();

        // Inicialmente no hay animacion de fade.

        if (_fadeComponent != null)
        {
            _fadeComponent.Transicion();
        }


        // Se inicializa los dedos.
        InicializaDedos();
        ISDEAD = false;
        ISWIN = false;
        _currentPage = 0;

        // para cuando exista el input 
        _input = GetComponent<InputManager>();

        // inicializacion del numero de pagina actual
        _currentPage = 0;

        // inducimos primer onEnter con valor dummy del estado
        _currentGameState = GameStates.END;

        _nextGameState = GameStates.LORE; // valor real inicial. 


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
