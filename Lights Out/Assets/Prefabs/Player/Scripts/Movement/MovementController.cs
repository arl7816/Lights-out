using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles core left - right movement for player
/// </summary>
[RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(PlayerController)),
    RequireComponent(typeof(PlayerState))]
public class MovementController : MonoBehaviour
{
    private Rigidbody2D rb;

    /// <summary>
    /// A float between [-1,1] indicating how the player is currently moving. 
    /// </summary>
    private float movementInput;

    PlayerController playerController;
    PlayerState playerState;

    private const string HORIZONTAL_INPUT_BINDING = "Horizontal";


    public void Start()
    {
        fetchComponents();
    }

    private void Update()
    {
        readInput();
    }

    private void FixedUpdate()
    {
        handleFacingDirection();
        applyMovement();
    }

    private void fetchComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerState = GetComponent<PlayerState>();
    }

    private void readInput()
    {
        movementInput = Input.GetAxisRaw(HORIZONTAL_INPUT_BINDING);
    }

    private void handleFacingDirection()
    {
        if (!playerState.facingRight && movementInput > 0){
            // the player is facing left and the input is going right => flip
            flip();
        } else if (playerState.facingRight && movementInput < 0){
            // the player is facing right and the input is going left => flip
            flip();
        }
    }

    private void flip()
    {
        playerState.notifyPlayerFlipped();
        playerState.facingRight = !playerState.facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void applyMovement()
    {
        // if the player is currently in a dashing state,
        // we don't want to override their x component.
        if (playerState.isDashing) return;

        rb.velocity = new Vector2(
            movementInput * playerController.getMovementSpeed(),
            rb.velocity.y);
    }
}
