using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test3 : MonoBehaviour
{
    [SerializeField] float length;
    Vector3 rawDirc, direction;
    float rawDicleng, distancetocenter;
    [SerializeField] float speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        //Random.RandomRange
        /*distancetocenter = Math.Sqrt(
            transform.position.x * transform.position.x 
            + 
            transform.position.y * transform.position.y);*/
        //length = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
        
    }
}
