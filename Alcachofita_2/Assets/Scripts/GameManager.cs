using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public enum GameStates { MAINMENU, GAME, END };
    const int NUM_DEDOS = 5;

    #region parameters
    // dedos por orden de corte
    // campo serializado para asignar los prefabs en el editor
    [SerializeField] 
    private GameObject PULGAR,
                       INDICE,
                       CORAZON,
                       ANULAR,
                       MENIQUE;

    // ARRAY DE DEDALOS
    // inicialmente se tienen 5 dedos
    public GameObject[] dedos = new GameObject[NUM_DEDOS];
    #endregion

    #region references
    // aun no hay nada je
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
    private int _nextDedo;
    public int NextDedo { get { return _nextDedo; } }
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

        Debug.Log("Nosss encontramoS en el eStado:" + _currentGameState);
    }

    // ---- updateState ----
    // sobretodo para input y ui y cosas asi ?????
    public void updateState(GameStates state)
    {
        
    }
    #endregion

    #region METODOS DE DEDOS
    // ---- InicializaVidas ----
    // settea cada indice del array con su dedo correspondiente
    // en orden de cortado
    private void InicializaVidas()
    {
        _nextDedo = 0;
        dedos[0] = PULGAR;
        dedos[1] = INDICE;
        dedos[2] = CORAZON;
        dedos[3] = ANULAR;
        dedos[4] = MENIQUE;
    }

    // ---- QuitaDedo ----
    // modifica el array sin el ultimo dedo a cortar
    // borra del array el nextDedo que debe de actualizarse siempre
    public GameObject[] QuitaDedo()
    {
        // Siempre y cuando el índice sea menor que dedos.Length...
        if(_nextDedo < dedos.Length)
        {
            // Se desactiva el dedo actual (de momento, luego hará lo del ragdoll y al salir de pantalla DESACTIVAR).
            dedos[_nextDedo].active = false;

            // Siguiente dedo.
            _nextDedo++;
        }

        return dedos;
    }

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
        // Se inicializa las vidas.
        InicializaVidas();

        // para cuando exista el input 
        _input = GetComponent<InputManager>();

        // Set active todos los dedalos.
        for(int i = 0; i < dedos.Length; i++)
        {
            dedos[i].SetActive(true); 
        }
        
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