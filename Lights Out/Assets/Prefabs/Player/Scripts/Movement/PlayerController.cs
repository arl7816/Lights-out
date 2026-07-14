using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Acts as the general manager for all communication between different aspects of the player movement.
This also holds the internal state machine for the player movement.

*/

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;
    public float getMovementSpeed() => movementSpeed;

    [SerializeField]
    private float jumpForce = 100f;
    public float getJumpForce() => jumpForce; 

    [SerializeField]
    private float dashForce = 120f;
    public float getDashForce() => dashForce;

    [SerializeField]
    private float dashingTime = 1f;
    public float getDashingTime() => dashingTime;

    [SerializeField]
    private float heavyFallGravityScale = 10f;
    public float getHeavyFallGravityScale() => heavyFallGravityScale;

    [SerializeField]
    private float maxFallSpeed = 20f;
    public float getMaxFallSpeed() => maxFallSpeed; 
}
