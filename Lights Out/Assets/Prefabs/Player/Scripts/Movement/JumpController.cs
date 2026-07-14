using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: would be nice if the allowed number of jumps was adjustable
// instead of one double jump, we have "airborn jumps". 

[RequireComponent(typeof(BoxRaycasting)),
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(PlayerController)),
    RequireComponent(typeof(PlayerState))]
public class JumpController : MonoBehaviour
{
    // required components attached to the game object
    private BoxRaycasting collisionDetector;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private PlayerState playerState;

    // helper variables
    private bool canDoubleJump = true;
    private bool jumpRequested = false;

    private void Awake()
    {
        fetchComponents();
    }

    private void Update()
    {
        readInput();
    }

    private void FixedUpdate()
    {
        handleJump();
    }

    private void fetchComponents()
    {
        collisionDetector = GetComponent<BoxRaycasting>();
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerState = GetComponent<PlayerState>();
    }

    private void readInput()
    {
        if (Input.GetButtonDown("Jump")) jumpRequested = true;
    }

    private void handleJump()
    {
        if (!jumpRequested) return;
        jumpRequested = false;
        tryJump();
    }

    private void tryJump()
    {
        // check directly to see if the player is grounded. 
        // Don't need any werid event mis-matches happening
        if (collisionDetector.IsGrounded){
            // when on the ground, the player gets a free jump and can jump again 
            canDoubleJump = true;
            performJump();
        }else if (!collisionDetector.IsGrounded && canDoubleJump){
            // if in the air and player has a double jump left, they use it up
            canDoubleJump = false;
            performJump();
        }
    }

    private void performJump()
    {
        // notify listeners that player is jumping
        playerState.notifyStartJump();

        // reset vertical velocity back to zero. 
        // if the player if falling we have a neg y thus the force is reduced
        // to get the same force impulse, we must reset the y component. 
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * playerController.getJumpForce(), ForceMode2D.Impulse);
    }

}
