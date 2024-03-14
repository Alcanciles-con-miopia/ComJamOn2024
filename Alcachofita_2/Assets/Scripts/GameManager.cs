using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { INTRO, GAME, END };

    #region references
    // aun no hay nada je
    #endregion

    #region properties
    // ESTADOS
    private static GameManager _instance;
    private GameStates _currentGameState;
    private GameStates _nextGameState;
    public static GameManager Instance { get { return _instance; } }
    public GameStates CurrentState { get { return _currentGameState; } }
    public GameStates NextState { get { return _nextGameState; } }
    #endregion

    public void RequestSateChange(GameStates newState)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
