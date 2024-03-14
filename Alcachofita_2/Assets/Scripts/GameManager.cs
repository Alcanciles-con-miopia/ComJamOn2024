using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public enum GameStates { MAINMENU, GAME, END };

    #region parameters
    public GameObject gameOver, indice, pulgar, corazon, anular, menique;
    public static int dedos;
    #endregion

    #region references
    // aun no hay nada je
    #endregion

    #region properties
    // GAMEMANAGER
    private static GameManager _gameManager;
    public static GameManager Instance { get { return _gameManager; } }

    // INPUT
    /*
    private InputManager _input;
    public InputManager Input { get { return _input; } }
    */

    // ESTADOS
    private GameStates _currentGameState;
    public GameStates CurrentState { get { return _currentGameState; } }

    private GameStates _nextGameState;
    public GameStates NextState { get { return _nextGameState; } }
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
        // para cuando exista el input 
        //_input = GetComponent<InputManager>();

        // Inicialmente disponemos de 5 dedos.
        dedos = 5;

        // Todos los dedos están activos al comienzo.
        indice.gameObject.SetActive(true);
        pulgar.gameObject.SetActive(true);
        corazon.gameObject.SetActive(true);
        anular.gameObject.SetActive(true);
        menique.gameObject.SetActive(true);

        // No hay gameover al incio.
        gameOver.gameObject.SetActive(false);
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

        while (dedos >= 0)
        {
            switch(dedos)
            {
                case 4: pulgar.gameObject.SetActive(false); break;
                case 3: indice.gameObject.SetActive(false); break;
                case 2: corazon.gameObject.SetActive(false); break;
                case 1: anular.gameObject.SetActive(false); break;
                case 0: 
                    menique.gameObject.SetActive(false);
                    gameOver.gameObject.SetActive(true);
                    break; 
            }
        }
    }
}