using UnityEngine;

public class ShakeComponent : MonoBehaviour
{
    private float shakeSpeed = 0f;
    [SerializeField] private float shakeFactor = 0.1f;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var xOffset = Mathf.PerlinNoise(Time.time * shakeSpeed, 0);
        var yOffset = Mathf.PerlinNoise(0, Time.time * shakeSpeed);

        transform.position = originalPosition + new Vector3(xOffset, yOffset, 0) * shakeFactor;
    }

    // este metodo esta heho para que el "resta vida" lo llame y aumente la velocidad de temblor
    public void ShakeSpeedChanger(float valueAdded)
    {
        shakeSpeed += valueAdded;
    }

}
