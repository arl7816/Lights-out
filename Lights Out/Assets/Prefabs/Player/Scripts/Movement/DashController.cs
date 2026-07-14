using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(PlayerController)),
    RequireComponent(typeof(PlayerState)),
    RequireComponent(typeof(BoxRaycasting)),
    RequireComponent(typeof(TrailRenderer))]
public class DashController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxRaycasting collisionDetector;
    private PlayerController playerController;
    private PlayerState playerState;
    private TrailRenderer dashTrail; 

    private bool canDash = true;
    private bool dashRequested = false;

    // holds a ref to the performDash method to let us cancel it
    private IEnumerator performDashRef;

    private void Awake()
    {
        fetchComponents();
        performDashRef = performDash();
    }

    private void OnEnable()
    {
        collisionDetector.GroundEntered += resetCanDash;
        collisionDetector.RightWallEntered += cancelDash;
        collisionDetector.LeftWallEntered += cancelDash;
        playerState.jumpStarted += cancelDash;
        playerState.playerFlipped += cancelDash;
    }

    private void OnDisable()
    {
        collisionDetector.GroundEntered -= resetCanDash;
        collisionDetector.RightWallEntered += cancelDash;
        collisionDetector.LeftWallEntered += cancelDash;
        playerState.jumpStarted -= cancelDash;
        playerState.playerFlipped -= cancelDash;
    }

    private void Update()
    {
        readInput();
    }

    private void FixedUpdate()
    {
        handleDash();
    }

    private void fetchComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionDetector = GetComponent<BoxRaycasting>();
        playerController = GetComponent<PlayerController>();
        playerState = GetComponent<PlayerState>();
        dashTrail = GetComponent<TrailRenderer>();
    }

    private void resetCanDash()
    {
        // if the player has touched the ground, they can dash again
        canDash = true;
        cancelDash();
    }

    private void cancelDash()
    {
        if (performDashRef != null)
        {
            StopCoroutine(performDashRef);
        }
        endDashState();
    }

    private void readInput()
    {
        // TODO, for testing only, need to create a dedicated input layer
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashRequested = true;
        }
    }

    private void handleDash()
    {
        if (!dashRequested) return;

        dashRequested = false; 
        tryDash();
    }

    private void tryDash()
    {
        // if player is grounded, no dash
        if (collisionDetector.IsGrounded) return;

        // if player has no dash left, no dash
        if (!canDash) return;

        // if the player is already dashing, no point in continuing the dash
        if (playerState.isDashing) return;

        // the player must be able to dash, do it
        performDashRef = performDash();
        StartCoroutine(
            performDashRef
        );
    }

    private IEnumerator performDash()
    {
        startDashState();
        
        int dashDirection = playerState.facingRight ? 1: -1;
        rb.AddForce(Vector2.right * playerController.getDashForce() * dashDirection, ForceMode2D.Impulse);
        yield return new WaitForSeconds(playerController.getDashingTime());
        
        endDashState();
    }

    private void startDashState()
    {
        playerState.notifyPlayerDashed();
        playerState.isDashing = true;
        dashTrail.emitting = true;
        canDash = false;
    }

    private void endDashState()
    {
        dashTrail.emitting = false;
        playerState.isDashing = false;
    }
}
