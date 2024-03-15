using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollComponent : MonoBehaviour
{
    
    private Rigidbody2D _myRigidBody;
    private bool separa = false;
    
    [SerializeField] private float fuerzaDedalo = 100f;
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
        _myRigidBody.bodyType = RigidbodyType2D.Dynamic;
        _myRigidBody.AddForce(transform.up * fuerzaDedalo);


    }
    
}

