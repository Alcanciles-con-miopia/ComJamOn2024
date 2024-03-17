using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    

    // probando singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
}
