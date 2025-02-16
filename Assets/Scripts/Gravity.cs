using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody2D rb;
    Movement playerMovement;
    new ParticleSystem particleSystem;
    ParticleSystem.MainModule mainModule;

    [Header("Particles Color")]
    [SerializeField] Color riseColor;
    [SerializeField] Color peakColor;
    [SerializeField] Color descentColor;
    [SerializeField] Color groundColor = Color.white;

    bool isGrounded;
    bool isJumping;
    float verticalVelocity;
    void Start()
    {
        Transform childTransform = transform.Find("GameObject/Trail");
        particleSystem = childTransform.GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        mainModule = particleSystem.main;
        playerMovement = GetComponent<Movement>();

    }

    private void Update()
    {
        isJumping = playerMovement.isJumping;
        isGrounded = playerMovement.isGrounded;
        verticalVelocity = rb.velocity.y;
        Particles();
    }

    private void FixedUpdate()
    {
        PlayerGravity();

    }

    void PlayerGravity()
    {

        //ON GROUND
        if (isGrounded)
        {
            rb.gravityScale = playerMovement.stats.defaultGravity;
        }

        //RISE
        if (playerMovement.isJumping && verticalVelocity > 0)
        {
            rb.gravityScale = playerMovement.stats.defaultGravity;
        }

        //PEAK
        if (isGrounded == false && isJumping == false)
        {
            rb.gravityScale = playerMovement.stats.peakGravity;
        }

        //DESCENT
        if (verticalVelocity < 0 && playerMovement.isGrounded == false)
        {
            rb.gravityScale = playerMovement.stats.fallingGravity;
        }
    }

    void Particles()
    {

        //ON GROUND
        if (isGrounded)
        {
            mainModule.startColor = groundColor;
        }

        //RISE
        if (playerMovement.isJumping && verticalVelocity > 0)
        {
            mainModule.startColor = riseColor;
        }

        //PEAK
        if (isGrounded == false && isJumping == false)
        {
            mainModule.startColor = peakColor;
        }

        //DESCENT
        if (verticalVelocity < 0 && playerMovement.isGrounded == false)
        {
            mainModule.startColor = descentColor;
        }
    }

}
