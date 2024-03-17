using UnityEngine;

public class SFXComponent : MonoBehaviour
{
    // probando singleton
    private static SFXComponent _instance;
    public static SFXComponent Instance { get { return _instance; } }


    [SerializeField]
    private AudioClip[] _sfx;
    private AudioSource _audioSource;

    /// <summary>
    /// ----ARRAY DE SFX--- 
    /// 0 --> blink 
    /// 1 --> boing
    /// 2 --> cabagge rip (arrancar dedo)
    /// 3 --> guts dragging (pintar)
    /// 4 --> paper (pasar pagina)
    /// 5 --> paperwipe (borrar)
    /// </summary>

    void Awake()
    {
        _instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    // metodos generales player

    public void StopAll()
    {
        for(int i = 0; i < _sfx.Length; i++)
        {
            if (_audioSource != null)
            {
                _audioSource.Stop();
            }
        }
    }

    public void SFXPlayer(int i)
    {
        if (_sfx[i] != null)
        {
            _audioSource.clip = _sfx[(i)];
            _audioSource.Play();
        }
    }
    public void SFXPlayerStop(int i)
    {
        if (_sfx[i] != null)
        {
            _audioSource.Stop();
        }
    }

    // te dice si el sonido esta sonando o no en el player
    public bool isPlayingSFX(int i)
    {
        return _audioSource.isPlaying;
    }
    
}
