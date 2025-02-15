using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Movement Parameters", menuName = "Movement Parameters")]
public class Stats : ScriptableObject
{
    public float acceleration;
    public float maxSpeed;
    public float maxJumpTime;
    public float jumpForce;
    public float riseGravity;
    public float peakGravity;
    public float descentGravity;
    public float onGroundGravity;
}
