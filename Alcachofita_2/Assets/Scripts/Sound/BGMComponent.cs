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
    /// 0 --> radio (intro)
    /// 1 --> mainMenu 
    /// 2 --> enricocaputo
    /// 3 --> Quiero ser libre
    /// 4 --> Quiero ser libre (con voces) creditos
    /// </summary>

    #region methods
    public void PlayBGM(int i)
    {
        // si existe en el array lo pone
        if (i < _bgm.Length && _bgm[i] != null)
        {
            _audioSource.clip = _bgm[i];
            _audioSource.Play();
        }
    }

    public void StopAll()
    {
        for (int i = 0; i < _bgm.Length; i++)
        {
            _audioSource.Stop();
        }
    }

    public bool IsBGMPlaying(int i)
    {
        return _audioSource.isPlaying;
    }
    public void StopBGM(int i)
    {
        // si esta sonando lo para
        if (i < _bgm.Length)
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    #endregion

    private void Start()
    {
        _currentBGM = 1;
        _audioSource = GetComponent<AudioSource>();
        GameManager.Instance.RegisterBGM(this);
    }

    private void Update()
    {

    }
}
