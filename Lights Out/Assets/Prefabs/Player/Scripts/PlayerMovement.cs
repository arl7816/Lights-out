using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // sets up the variables
    private Rigidbody2D rb;
    private float move_input;
    private bool facing_right = true;
    private int extra_jumps = 1;
    private bool isfalling = false;
    private bool onwall = false;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jump_force;
    [SerializeField]
    private int jumps;

    [SerializeField]
    private Camera cam;

    private bool floatMode = false;

    // dashing stuff
    public bool canDash = true;
    public bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;
    private TrailRenderer tr;

    public void changeMoveMode()
    {
        floatMode = !floatMode;

        rb.gravityScale = floatMode ? 0 : 1;
        foreach (Collider2D collider in gameObject.GetComponents<Collider2D>())
        {
            collider.enabled = !floatMode;
        }
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        extra_jumps = jumps;
        tr = gameObject.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKey("s")) {
            rb.gravityScale = 5;
        }else
        {
            rb.gravityScale = 1;
        }

        // figures out if the player is on a wall or not
        BoxRaycasting rays = GetComponent<BoxRaycasting>();
        if (rays.collisionRight == true && rays.collisionDown == false || rays.collisionLeft == true && rays.collisionDown == false)
        {
            onwall = true;
        }
        else
        {
            onwall = false;
        }

        // jumps if you have more than -1 jumps remaining
        if (!floatMode)
        {
            if ((Input.GetKeyDown("up")|| Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1") || Input.GetKeyDown("w")) && extra_jumps >= 0)
            {
                rb.velocity = Vector2.up * jump_force;
                isfalling = false;
                extra_jumps--;
            }
        }

        // figures out if the player is falling
        if (rb.velocity.y < -0.1)
        {
            isfalling = true;
        }
        else
        {
            isfalling = false;
        }

        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
    }

    private void FixedUpdate()
    {

        if (isDashing) { return; }

        // moves charcter right or left 
        move_input = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move_input * speed, rb.velocity.y);

        if (floatMode)
        {
            rb.velocity = new Vector2(move_input * speed, Input.GetAxis("Vertical") * speed);
        }

        // decides whether or not to flip chacter
        if (facing_right == false && move_input > 0)
        {
            flip();
        }
        else if (facing_right && move_input < 0)
        {
            flip();
        }
    }

    public void take_away_jump()
    {
        extra_jumps--;
    }

    void flip()
    {
        // flips the player
        facing_right = !facing_right;

        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // checks to see if the player should get there jumps back (anything other than ceiling)
        BoxRaycasting rays = GetComponent<BoxRaycasting>();
        if (rays.collisionLeft || rays.collisionRight || rays.collisionDown)
        {
            extra_jumps = jumps;
            canDash = true;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        int direction = facing_right ? 1 : -1;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower * direction, 0);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
    }
}
