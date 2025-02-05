using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class test4 : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (targetPosition * rb.velocity).normalized;


        if (Vector3.Distance(transform.position, targetPosition) > 0.1f) 
        {
            rb.velocity = direction * acceleration * Time.deltaTime;
        }
        else
        {
            rb.velocity = targetPosition;
        }
    }
}

