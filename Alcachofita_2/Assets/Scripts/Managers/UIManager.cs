using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // References
    [SerializeField] // sigue el orden de los estados del gamemanager
    private GameObject[] menus;
    [SerializeField]
    private TMP_Text acertijo_number;
    [SerializeField]
    private Image runa;


    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject winText;

    [SerializeField]
    private GameObject overText;
    [SerializeField]
    private GameObject retryButton;

    // Properties
    private GameManager.GameStates _activeMenu;

    #region ON CLICKS
    /// <param name="state"></param>
    public void RequestStateChange(GameManager.GameStates state)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.requestSateChange(state);
    }

    /// Metodo para el onClick de los botones, para pasar al juego
    public void GoToGame()
    {
        RequestStateChange(GameManager.GameStates.GAME); // referenciando al gamemanager (importante! si no no cambia de estado)
        //...
    }

    // Metodo para pasar de pagina
    public void TurnPage()
    {
        if (GameManager.Instance != null)
        {
            // cambia de páginas
            GameManager.Instance.NextPage();
            Debug.Log(GameManager.Instance.CurrentPage);

            //percent.text = GameManager.Instance.GetPercent() + "%";
        }
    }

    public void ErasePage()
    { // ESTO ES UNA PUTA MIERDA XD 
        if (GameManager.Instance != null && GameManager.Instance.Input != null)
        {
            if (GameManager.Instance.Input.DrawingComponent != null)
            {
                GameManager.Instance.Input.DrawingComponent.EraseDrawing();
            }
        }
    }

    /// Metodo para el onClick de los botones, para pasar al final del juego
    public void GoToEnding()
    {
        RequestStateChange(GameManager.GameStates.END); // referenciando al gamemanager (importante! si no no cambia de estado)
    }

    /// Metodo para el onClick de los botones, para reintar 
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// Metodo para el onClick de los botones, para salir del juego
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    public void ChangeRuna(Sprite _runa)
    {
        runa.sprite = _runa;
    }

    public void ChangeAcertijoNumber(int acertijo)
    {
        string romano = "";
        switch (acertijo)
        { // XDDDDDDDDDDDDDDDDDDDDDDDDD
            case 0: romano = "I"; break;
            case 1: romano = "II"; break;
            case 2: romano = "III"; break;
            case 3: romano = "IV"; break;
            case 4: romano = "V"; break;
            case 5: romano = "VI"; break;
            case 6: romano = "VII"; break;
            case 7: romano = "VIII"; break;
            case 8: romano = "IX"; break;
            case 9: romano = "X"; break;
            case 10: romano = "XI"; break;
            default: break;
        }
        acertijo_number.text = romano;
    }

    public void DisableRune()
    {
        runa.enabled = false;
    }

    public void SetMenu(GameManager.GameStates newMenu)
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        _activeMenu = newMenu;
        menus[(int)_activeMenu].SetActive(true);
    }

    public void SetWin()
    {
        winScreen.SetActive(true);
        winText.SetActive(true);
        overText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterUIManager(this);
    }
}
