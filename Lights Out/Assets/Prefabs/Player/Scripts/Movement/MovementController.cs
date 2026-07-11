using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles core left - right movement for player
/// </summary>
public class MovementController : MonoBehaviour
{
    // The following are assumed to be attached to the player
    private Rigidbody2D rb;

    /// <summary>
    /// A float between [-1,1] indicating how the player is currently moving. 
    /// </summary>
    private float movementInput;

    // THIS NEEDS TO BE MOVED TO THE PLAYERCONTROLLER LATER
    [SerializeField]
    private float movementSpeed;

    private bool facingRight = true;


    private void Update()
    {
        readInput();
    }

    private void FixedUpdate()
    {
        handleFacingDirection();
        applyMovement();
    }

    private void readInput()
    {
        movementInput = Input.GetAxisRaw("Horizontal");
    }

    private void handleFacingDirection()
    {
        if (!facingRight && movementInput > 0){
            // the player is facing left and the input is going right => flip
            flip();
        } else if (facingRight && movementInput < 0){
            // the player is facing right and the input is going left => flip
            flip();
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void applyMovement()
    {
        rb.velocity = new Vector2(
            movementInput * movementSpeed,
            rb.velocity.y);
    }
}
