using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    Rigidbody rb;
    public float jumpForce = 2;
    public event System.Action Jumped;
    PlayerGroundCheck gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GetComponent<PlayerGroundCheck>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gc.isGrounded)
        {
            rb.AddForce(Vector3.up * 2 * jumpForce, ForceMode.Impulse);
            Jumped?.Invoke();
        }
    }
}
