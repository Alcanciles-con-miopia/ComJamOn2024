using UnityEngine;

public class MeniqueComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.requestSateChange(GameManager.GameStates.END);
        }
    }

    private void Start()
    {

    }
}
