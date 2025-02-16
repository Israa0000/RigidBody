using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Movement Parameters", menuName = "Movement Parameters")]
public class Stats : ScriptableObject
{
    [Header("On Ground stats")]
    public float groundAcceleration;
    public float maxGroundHorizontalSpeed;
    public float groundHorizontalFriction;

    [Header("On Air stats")]
    public float airAcceleration;
    public float airHorizontalFriction;
    public float maxAirHorizontalSpeed;
    public float yVelocityLowGravityThreshold;
    public int onAirJumps;

    [Header("Jumps stats")]
    public float jumpForce;
    public float maxJumpTime;
    public float maxFallSpeed;

    [Header("Gravity stats")]
    public float defaultGravity;
    public float peakGravity;
    public float fallingGravity;
}
