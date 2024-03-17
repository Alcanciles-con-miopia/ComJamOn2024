using UnityEngine;

public class SFXComponent : MonoBehaviour
{
    // probando singleton
    private static SFXComponent _instance;
    public static SFXComponent Instance { get { return _instance; } }


    [SerializeField]
    private AudioSource[] _sfx;

    /// <summary>
    /// ----ARRAY DE SFX--- 
    /// 0 --> blink 
    /// 1 --> boing
    /// 2 --> cabagge rip (arrancar dedo)
    /// 3 --> guts dragging (pintar)
    /// 3 --> paper (pasar pagina)
    /// </summary>

    void Awake()
    {
        _instance = this;
    }

    // metodos generales player

    public void StopAll()
    {
        for(int i = 0; i < _sfx.Length; i++)
        {
            _sfx[i].Stop();
        }
    }

    public void SFXPlayer(int i)
    {
        if (_sfx[i] != null)
        {
            _sfx[i].Play();
        }
    }
    public void SFXPlayerStop(int i)
    {
        if (_sfx[i] != null)
        {
            _sfx[i].Stop();
        }
    }

    // te dice si el sonido esta sonando o no en el player
    public bool isPlayingSFX(int i)
    {
        return _sfx[i].isPlaying;
    }
    
}
