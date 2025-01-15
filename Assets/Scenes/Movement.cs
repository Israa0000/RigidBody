using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float acceleration = 6f;
    [SerializeField] float maxSpeed = 1f; //velocidad maxima
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;
    [SerializeField] float maxJumpTime = 1f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isJumping;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
    }

    void Update()
    {
    float jumpTime;
    bool jumpInput = Input.GetKeyDown(KeyCode.Space);

        Vector2 horizontalInput = new Vector2 (0,0);

        isGrounded = Physics2D.Raycast(
            gameObject.transform.position,
            Vector2.down,
            0.7f,
            ground);

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += new Vector2(1, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput += new Vector2(-1, 0);
        }

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }

        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (isGrounded && jumpInput)
        {
            isJumping = true; 
            jumpTime = 0f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (isJumping)
        {
            jumpTime = Time.deltaTime;

            if (jumpTime > maxJumpTime)
            {
                isJumping = false;
            }
        }



        rb.velocity += horizontalInput * acceleration * Time.deltaTime;

       // if (isGround = false && Input.GetKey(KeyCode.Space))
       // {
         //   movementInput = movementInput + new Vector2(0, 5);
        //}
    }
}
