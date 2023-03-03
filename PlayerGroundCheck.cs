using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public float distanceThreshold = .15f;
    public bool isGrounded = true;
    public event System.Action Grounded;
    const float OriginOffset = .01f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;
    float RaycastDistance => distanceThreshold + OriginOffset;

    // Update is called once per frame
    void Update()
    {
        bool isGroundedNow = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold*2);

        if (isGrounded && !isGroundedNow)
        {
            Grounded?.Invoke();
        }

        isGrounded = isGroundedNow;
    }
}
