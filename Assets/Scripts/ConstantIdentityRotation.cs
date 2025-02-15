using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantIdentityRotation : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
