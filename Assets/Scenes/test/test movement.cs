using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmovement : MonoBehaviour
{
    [SerializeField] float speed = 0F;

    void Start()
    {
        transform.up = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerMovement = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            playerMovement += Vector3.right;
        }

        if (Input.GetKey(KeyCode.W))
        {
            playerMovement += Vector3.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerMovement += Vector3.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerMovement += Vector3.down;
        }

        playerMovement.Normalize();

        transform.position += playerMovement * Time.deltaTime * speed;

    }
}
