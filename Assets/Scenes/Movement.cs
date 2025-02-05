using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float acceleration = 6f;
    [SerializeField] float maxSpeed = 1f; 
    [SerializeField] bool isGrounded;
    [SerializeField] float maxJumpTime = 1f;
    [SerializeField] float jumpForce;
    [SerializeField] bool isJumping;
    [SerializeField] float jumpTime;
    [SerializeField] LayerMask ground;
    [SerializeField] Gradient gradient;
    [SerializeField] int currentJumps = 0;
    [SerializeField] int maxJump = 3;
    SpriteRenderer spriteRenderer;
    [SerializeField] float raycastDistance = 0.51f;
    [SerializeField] bool a = false;
    [SerializeField] ParticleSystem landingDust;
    [SerializeField] ParticleSystem trail;
    [SerializeField] bool wasGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();

        //movimiento particulas
    
    }

    void Update()
    {
        Vector2 horizontalInput = new Vector2 (0,0);

        isGrounded = Physics2D.Raycast(
            gameObject.transform.position,
            Vector2.down,
            raycastDistance,
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

        rb.velocity += horizontalInput * acceleration * Time.deltaTime;


        //Salto
        bool jumpInputStart = Input.GetKeyDown(KeyCode.Space);
        bool jumpInput = Input.GetKey(KeyCode.Space);
        bool jumpInputEnd = Input.GetKeyUp(KeyCode.Space);

        // comprobar y almacenar si puedo comenzar un salto (isJumping)
        if (jumpInputStart && (isGrounded || currentJumps < maxJump))
        {
            if (isGrounded)
            {
                currentJumps = 1;
            }
            currentJumps++;
            isJumping = true;
            jumpTime = 0;
            if (a) a = false;
        }

        if (isGrounded)
        {
            currentJumps = 0;
        }

        if (isJumping)
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            jumpTime += Time.deltaTime;
        }

        // comprobar si tiene que terminar de aplicarse la fuerza del salto
        if (jumpInputEnd || jumpTime > maxJumpTime)
        {
            isJumping = false;
        }

        //color
        float normalizedJumpCount = Mathf.InverseLerp(0, maxJump, currentJumps); //interpolacion lineal inversa// modificar la formula 
        spriteRenderer.color = gradient.Evaluate(normalizedJumpCount);

        //particulas
        CheckGroundDust();

        //almacenar isGrounded en el ultimo frame
        wasGrounded = isGrounded;
    }
    void CheckGroundDust()
    {
        if (isGrounded && !wasGrounded)
        {
            landingDust.Play();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.01f);
        Gizmos.DrawLine(
            gameObject.transform.position,
            gameObject.transform.position + Vector3.down * raycastDistance
            );
    }
}
