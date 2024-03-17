using UnityEngine;
using static GameManager;

public class BGMComponent : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _bgm;

    private AudioSource _audioSource;

    #region properties
    private int _currentBGM;
    private int _nextBGM;
    
    [SerializeField]
    private bool _canPlay;
    public bool CanPlay { get { return _canPlay; } set { _canPlay = value; } }

    #endregion

    /// <summary>
    /// Indice
    /// 
    /// 0 --> enricoCaputo (gameplay)
    /// 1 --> mainMenu
    /// 2 --> Quiero ser libre (sin voces)
    /// </summary>

    #region methods
    public void PlayBGM(int i)
    {
        // si existe en el array lo pone
        if (_bgm[i] != null)
        {
            _audioSource.clip = _bgm[i];
            _audioSource.Play();
        }
    }

    public void StopAll()
    {
        
        _audioSource.Stop();
        
    }

    public bool IsBGMPlaying(int i)
    {
        return _audioSource.isPlaying;
    }
    public void StopBGM(int i)
    {
        // si esta sonando lo para
        if (_audioSource.isPlaying)
        {
            _audioSource.clip = _bgm[i];
            _audioSource.Stop();
        }
    }

    #endregion

    private void BGMManager()
    {
        if (Instance != null)
        {            switch (Instance.CurrentState) // Diferentes comportamientos según estado al que se entra
            {
            case GameStates.MAINMENU:                    //      *MENU INICIAL*
                _nextBGM = 1;
                break;
            case GameStates.GAME:                       //       *JUEGO*
                _nextBGM = 0;
                break;
            case GameStates.END:                        //       *FINAL DEMONIO*
                _nextBGM = 2; 
                break;
            }
        }
    }

    private void Awake()
    {
        GameManager.Instance.RegisterBGM(this);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentBGM = 1;
    }

    private void Update()
    {
        BGMManager();
        
        if (_currentBGM != _nextBGM)
        {

            for(int i = 0; i < _bgm.Length - 1; i++)
            {
                _audioSource.clip = _bgm[i];
                _audioSource.Stop();
            }
            StopBGM(_currentBGM);
            _currentBGM = _nextBGM;
            if (_currentBGM >= 0)
            {
                PlayBGM(_currentBGM);
            }
        }
    }
}
