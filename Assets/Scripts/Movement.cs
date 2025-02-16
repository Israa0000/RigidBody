using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Stats stats;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [Header("Jump Settings")]
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool isJumping;
    [SerializeField] public bool wasGrounded;
    [SerializeField] public float jumpTime;
    public bool jumpInputStart;
    public bool jumpInput;
    public bool jumpInputEnd;
    [SerializeField] int currentJumps = 0;
    [SerializeField] int maxJump = 3;

    [Header("Contact Layer")]
    [SerializeField] LayerMask ground;

    [Header("Color")]
    [SerializeField] Gradient gradient;

    [Header("Gizmo y Raycast")]
    [SerializeField] float raycastDistance = 0.5f;

    [Header("Particles")]
    [SerializeField] ParticleSystem landingDust;

    Vector2 horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        jumpInputStart = (Input.GetKeyDown(KeyCode.Space));
        jumpInput = Input.GetKey(KeyCode.Space);
        jumpInputEnd = Input.GetKeyUp(KeyCode.Space);

        horizontalInput = Vector2.zero;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += new Vector2(1, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput += new Vector2(-1, 0);
        }
        JumpColor();
        Jump();
        Move();
        wasGrounded = isGrounded;
    }
    void FixedUpdate()
    {
        CheckGroundStatus();
        CheckGroundDust();

        jumpInputStart = false;
        jumpInputEnd = false;
    }
    //RAYCAST
    void CheckGroundStatus()
    {
        isGrounded = Physics2D.Raycast(
            gameObject.transform.position,
            Vector2.down,
            raycastDistance,
            ground);

        if (isGrounded)
        {
            currentJumps = 0;
            isJumping = false;
        }
        
    }

    void Move()
    {
        if (isGrounded)
        {
            rb.velocity += horizontalInput * stats.groundAcceleration * Time.deltaTime;

            if (rb.velocity.x > stats.maxGroundHorizontalSpeed)
            {
                rb.velocity = new Vector2(stats.maxGroundHorizontalSpeed, rb.velocity.y);
            }

            if (rb.velocity.x < -stats.maxGroundHorizontalSpeed)
            {
                rb.velocity = new Vector2(-stats.maxGroundHorizontalSpeed, rb.velocity.y);
            }

            rb.velocity = new Vector2(rb.velocity.x * stats.groundHorizontalFriction, rb.velocity.y);
        }
        else
        {
            rb.velocity += horizontalInput * stats.airAcceleration * Time.deltaTime;

            if (rb.velocity.x > stats.maxAirHorizontalSpeed)
            {
                rb.velocity = new Vector2(stats.maxAirHorizontalSpeed, rb.velocity.y);
            }

            if (rb.velocity.x < -stats.maxAirHorizontalSpeed)
            {
                rb.velocity = new Vector2(-stats.maxAirHorizontalSpeed, rb.velocity.y);
            }

            rb.velocity = new Vector2(rb.velocity.x * stats.airHorizontalFriction, rb.velocity.y);

            if (rb.velocity.y < stats.yVelocityLowGravityThreshold)
            {
                rb.gravityScale = stats.fallingGravity;
            }
            else
            {
                rb.gravityScale = stats.defaultGravity;
            }
        }
    }


    public void Jump()
    {
        if (jumpInputStart && (isGrounded || currentJumps < stats.onAirJumps))
        {
            if (isGrounded)
            {
                currentJumps = 1;
            }
            currentJumps++;
            isJumping = true;
            jumpTime = 0;

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

        if (jumpInputEnd || jumpTime > stats.maxJumpTime)
        {
            isJumping = false;
        }
    }

    // COLOR
    public void JumpColor() {
        float normalizedJumpCount = Mathf.InverseLerp(0, maxJump, currentJumps); //interpolacion lineal inversa// modificar la formula 
        spriteRenderer.color = gradient.Evaluate(normalizedJumpCount);
    }
    
    // PARTICULAS
    public void CheckGroundDust()
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
