using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIManager : MonoBehaviour
{
    // References
    [SerializeField] // sigue el orden de los estados del gamemanager
    public GameObject[] menus;

    public void RequestStateChange(GameManager.GameStates state)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.requestSateChange(state);
    }

    public void GoToGame()
    {
        RequestStateChange(GameManager.GameStates.GAME); // referenciando al gamemanager (importante! si no no cambia de estado)
        //...
    }

   public void ExitGame()
    {
        Application.Quit();
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
