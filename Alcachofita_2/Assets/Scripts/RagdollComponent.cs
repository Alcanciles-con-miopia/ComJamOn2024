using UnityEngine;

public class RagdollComponent : MonoBehaviour
{

    private Rigidbody2D _myRigidBody;
    private bool separa = false;
    [SerializeField] Collider2D colision;

    [SerializeField] private float fuerzaDedalo = 100f;
    [SerializeField] private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterRagdoll(this);

        _myRigidBody = GetComponent<Rigidbody2D>();
        _myRigidBody.bodyType = RigidbodyType2D.Static;
    }

    public void SeparaDedo()
    {
        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
        _myRigidBody.bodyType = RigidbodyType2D.Dynamic;
        _myRigidBody.AddForce(transform.up * fuerzaDedalo);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

}

