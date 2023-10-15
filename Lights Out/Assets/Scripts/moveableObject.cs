using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveableObject : MonoBehaviour
{

    [SerializeField]
    private float speed = 1f;

    public bool upDown = false;

    [SerializeField]
    private bool movingRight_Up = true;

    [SerializeField]
    private Transform leftPoint; // also down

    [SerializeField]
    private Transform rightPoint; // also up

    [SerializeField]
    private float rayCastLength = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private bool checkCollision()
    {
        RaycastHit2D rightHit = new RaycastHit2D();
        RaycastHit2D leftHit = new RaycastHit2D();

        if (!upDown)
        {
            if (movingRight_Up)
            {
                rightHit = Physics2D.Raycast(rightPoint.position, rightPoint.right, rayCastLength);
                if (rightHit)
                {
                    if (rightHit.collider.gameObject.tag == "Player") { return false; }
                }

                return rightHit;
            }
            else
            {
                leftHit = Physics2D.Raycast(leftPoint.position, -leftPoint.right, rayCastLength);

                if (leftHit)
                {
                    if (leftHit.collider.gameObject.tag == "Player") { return false; }
                }

                return leftHit;
            }
            
        }
        else
        {
            if (movingRight_Up)
            {
                rightHit = Physics2D.Raycast(rightPoint.position, rightPoint.up, rayCastLength);

                if (rightHit)
                {
                    if (rightHit.collider.gameObject.tag == "Player") { return false; }
                }

                return rightHit;
            }
            else
            {
                leftHit = Physics2D.Raycast(leftPoint.position, -leftPoint.up, rayCastLength);

                if (leftHit)
                {
                    if (leftHit.collider.gameObject.tag == "Player") { return false; }
                }

                return leftHit;
            }
        }
    }

    private void FixedUpdate()
    {
        float deltaX = 0f;
        float deltaY = 0f;
        if (!upDown)
        {
            deltaX = (movingRight_Up) ? speed * Time.deltaTime : -speed * Time.deltaTime;
        }
        else
        {
            deltaY = (movingRight_Up) ? speed * Time.deltaTime : -speed * Time.deltaTime;
        }

        rb.velocity = new Vector2(deltaX, deltaY);

        if (checkCollision())
        {
            movingRight_Up = !movingRight_Up;
        }
    }
}
