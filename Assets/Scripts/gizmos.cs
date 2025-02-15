using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gizmos : MonoBehaviour
{
    [SerializeField] float groundCheckDistance = 0.6f;
    [SerializeField] Transform groundCheckOrigin;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = Physics2D.Raycast(
            groundCheckOrigin.position,
            Vector2.down,
            groundCheckDistance,
            ground);
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * groundCheckDistance
            );
    }
}
