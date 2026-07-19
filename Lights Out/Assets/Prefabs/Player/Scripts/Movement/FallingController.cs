using UnityEngine;

// QUESTION: 
/*
 * Do we want the fall to be instant or gradual
 * If gradual, dashing shouldnt be cancelled out
 * If instant, dash should be cancelled out no? 
 */

[RequireComponent(typeof(Rigidbody2D)),
 RequireComponent(typeof(PlayerController)),
 RequireComponent(typeof(PlayerState)),
 RequireComponent(typeof(BoxRaycasting))]
public class FallingController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxRaycasting collisionDetector;
    private PlayerController playerController;
    private PlayerState playerState;

    private bool requestedFalling;

    private float defaultGravity;

    private void Awake()
    {
        fetchComponents();
        setDefaultGravity();
    }

    private void Update()
    {
        readInput();
    }

    private void FixedUpdate()
    {
        handleInput();

        clampFallSpeed();
    }

    private void fetchComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        collisionDetector = GetComponent<BoxRaycasting>();
        playerController = GetComponent<PlayerController>();
        playerState = GetComponent<PlayerState>();
    }

    private void setDefaultGravity()
    {
        defaultGravity = rb.gravityScale;
    }

    private void readInput()
    {
        requestedFalling = Input.GetAxisRaw("Vertical") < 0;
    }

    private void handleInput()
    {
        if (requestedFalling){
            tryStartFalling();
        }else{
            stopFalling();
        }
    }

    private void tryStartFalling()
    {
        // Heavy fall should only be active while airborne.
        if (collisionDetector.IsGrounded){
            stopFalling();
            return;
        }

        if (playerState.isHeavyFalling) return;

        startFalling();
    }

    private void startFalling()
    {
        playerState.notifyPlayerStartedHeavyFall();
        playerState.isHeavyFalling = true;
        rb.gravityScale = playerController.getHeavyFallGravityScale();
    }

    private void stopFalling()
    {
        if (!playerState.isHeavyFalling) return;

        playerState.isHeavyFalling = false;
        rb.gravityScale = defaultGravity;
        playerState.notifyPlayerStoppedHeavyFall();
    }

    private void clampFallSpeed()
    {
        if (rb.velocity.y < -playerController.getMaxFallSpeed())
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                -playerController.getMaxFallSpeed());
        }
    }
}