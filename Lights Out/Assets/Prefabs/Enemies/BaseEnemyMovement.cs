using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class BaseEnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool facingRight = true;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float edgeDistance = 0.1f;

    [SerializeField]
    private GameObject edge;

    private Rigidbody2D rb;

    private float radius;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (!facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }

        radius = gameObject.GetComponentInChildren<Light2D>().pointLightOuterRadius;
    }

    private void Update()
    {
        if (!Physics2D.Raycast(edge.gameObject.transform.position, Vector2.down, edgeDistance))
        {
            flip();    
        }

        RaycastHit2D frontHit = Physics2D.Raycast(edge.gameObject.transform.position, Vector2.right, edgeDistance);
        if (frontHit && frontHit.collider.gameObject != this.gameObject
            && frontHit.collider.gameObject.tag != "Player")
        {
            flip();
        }

        float move_input = facingRight == true ? 1 : -1;

        rb.velocity = new Vector2(move_input * speed, rb.velocity.y);

        // work on from here my sorry guy
        Physics2D.OverlapCircle(gameObject.transform.position, radius, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //flip();
    }

    void flip()
    {
        transform.eulerAngles = new Vector3(0f, facingRight ? -180f: 0f, 0f);

        facingRight = !facingRight;

        //transform.Rotate(0f, 180f, 0f);
    }

}
