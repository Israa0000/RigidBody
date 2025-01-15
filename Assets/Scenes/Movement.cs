using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float acceleration = 6f;
    [SerializeField] float maxSpeed = 1f; //velocidad maxima
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
    }

    void Update()
    {
        Vector2 movementInput = new Vector2 (0,0);

        isGround = Physics2D.Raycast(
            gameObject.transform.position,
            Vector2.down,
            0.7f,
            ground);

        if (Input.GetKey(KeyCode.D))
        {
            movementInput += new Vector2(1, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementInput += new Vector2(-1, 0);
        }

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }

        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            movementInput = movementInput + new Vector2 (0,5);
        }
        rb.velocity += movementInput * acceleration * Time.deltaTime;
    }
}
