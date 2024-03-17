using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    

    // probando singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }


    private void Awake()
    {
    }

    private void Start()
    {
        
    }
}
