using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCenter : MonoBehaviour
{

    public float runspeed = 7;
    Rigidbody rb;
   
    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround;
    
    public float jumpForce = 2;
    public event System.Action Jumped;
    public float distanceThreshold = .15f;

    
    
    public event System.Action Grounded;
    const float OriginOffset = .01f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;
    float RaycastDistance => distanceThreshold + OriginOffset;

    public bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

         if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * 2 * jumpForce, ForceMode.Impulse);
            Jumped?.Invoke();
        }


            // ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

      

        // handle drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;


        






        float targetMovingSpeed = runspeed;

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);

        bool isGroundedNow = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold*2);

        if (isGrounded && !isGroundedNow)
        {
            Grounded?.Invoke();
        }

        isGrounded = isGroundedNow;
    }
}
