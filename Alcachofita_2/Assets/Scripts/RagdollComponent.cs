using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollComponent : MonoBehaviour
{
    private Rigidbody2D _myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterRagdoll(this);

        _myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void SeparaDedo()
    {
        Debug.Log("cacorra");

        _myRigidBody.AddForce(Vector2.up);
    }
}
