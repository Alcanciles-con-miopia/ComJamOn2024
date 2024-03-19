using System.Collections;
using UnityEngine;

public class SpreadComponent : MonoBehaviour
{
    private float timer = 0f;
    public float growTime = 6f;
    public float maxsize = 1.5f;
    private bool isMaxSize = false;


    public void MariaDoloresDeCospedal()
    {
        if (!isMaxSize)
        {
            StartCoroutine(Grow());
        }
    }

    private IEnumerator Grow()
    {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2(maxsize, maxsize);

        do
        {
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer / growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer < growTime);   
        isMaxSize = true;
    }
}
