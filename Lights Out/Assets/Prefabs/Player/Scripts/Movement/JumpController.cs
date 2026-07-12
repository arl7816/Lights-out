using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxRaycasting)),
    RequireComponent(typeof(Rigidbody2D))]
public class JumpController : MonoBehaviour
{
    private BoxRaycasting collisionDetector;
    private Rigidbody2D rb;

    private bool canJump = true;
    private int totalJumps = 1;
    private bool canDoubleJump = true;
    private bool jumpRequested = false;

    [SerializeField]
    private float jumpForce = 1f;

    private void Awake()
    {
        collisionDetector = GetComponent<BoxRaycasting>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        readInput();
    }

    private void FixedUpdate()
    {
        handleJump();
    }

    private void readInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Detected jump button");
            jumpRequested = true;
        }
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
            totalJumps = 1;
            performJump();
        }else if (!collisionDetector.IsGrounded && totalJumps != 0){
            totalJumps = 0;
            performJump();
        }
    }

    private void performJump()
    {
        canJump = false;
        // reset vertical velocity
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}
