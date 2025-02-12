using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Stats stats;
    Rigidbody2D rb;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isJumping;
    [SerializeField] float jumpTime;
    [SerializeField] LayerMask ground;
    [SerializeField] Gradient gradient;
    [SerializeField] int currentJumps = 0;
    [SerializeField] int maxJump = 3;
    SpriteRenderer spriteRenderer;
    [SerializeField] float raycastDistance = 0.4f;
    [SerializeField] bool a = false;
    [SerializeField] ParticleSystem landingDust;
    [SerializeField] bool wasGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckGroundDust();
        Jump();
        Move();
        JumpColor();
        wasGrounded = isGrounded;
    }

    //MOVIMIENTO HORIZONTAL
    void Move()
    {
        Vector2 horizontalInput = new Vector2(0, 0);

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

        if (rb.velocity.x > stats.maxSpeed)
        {
            rb.velocity = new Vector2(stats.maxSpeed, rb.velocity.y);
        }

        if (rb.velocity.x < -stats.maxSpeed)
        {
            rb.velocity = new Vector2(-stats.maxSpeed, rb.velocity.y);
        }
        
        rb.velocity += horizontalInput * stats.acceleration * Time.deltaTime;
    }
    void Jump()
    {
        // SALTO
        bool jumpInputStart = Input.GetKeyDown(KeyCode.Space);
        bool jumpInput = Input.GetKey(KeyCode.Space);
        bool jumpInputEnd = Input.GetKeyUp(KeyCode.Space);

        // COMPROBAR Y ALMACENAR SI PUEDO HACER UN SALTO (isJumping)
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
            rb.velocity = new Vector2(rb.velocity.x, stats.jumpForce);
            jumpTime += Time.deltaTime;
        }

        // COMPROBAR SI TIENE QUE TERMINAR DE APLICARSE LA FUERZA DE SALTO
        if (jumpInputEnd || jumpTime > stats.maxJumpTime)
        {
            isJumping = false;
        }
    }

    // COLOR
    void JumpColor() {
        float normalizedJumpCount = Mathf.InverseLerp(0, maxJump, currentJumps); //interpolacion lineal inversa// modificar la formula 
        spriteRenderer.color = gradient.Evaluate(normalizedJumpCount);
    }
    
    // PARTICULAS
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
    public void SetMovementProfile(Stats newStats)
    {
        stats = newStats;
    }
}
