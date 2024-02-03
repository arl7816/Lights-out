using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Alex Lee
/// </summary>
public class BoxRaycasting : MonoBehaviour
{
    //fields
    public bool collisionUp;
    public bool collisionDown;
    public bool collisionLeft;
    public bool collisionRight;

    //show rays debug
    public bool showRays = false;

    //ray cast fields
    public float rayDistance;


    //floor
    public Transform bottomLeft;
    public Transform bottomRight;

    //wall Left
    public Transform leftTop;
    public Transform leftBottom;

    //wall right
    public Transform rightTop;
    public Transform rightBottom;

    //ceiling
    public Transform topLeft;
    public Transform topRight;

    // Update is called once per frame
    void Update()
    {
        checkCollision();
    }

    //debug function
    void drawRaycast()
    {

    }

    void checkCollision()
    {
        //Collision Detection (with Raycast)

        // create floor raycast
        RaycastHit2D LFloorRay = Physics2D.Raycast(bottomLeft.position, -(bottomLeft.up), rayDistance);
        RaycastHit2D RFloorRay = Physics2D.Raycast(bottomRight.position, -(bottomRight.up), rayDistance);

        // creates ceiling raycasts
        RaycastHit2D LCeiling = Physics2D.Raycast(topLeft.position, topLeft.up, rayDistance);
        RaycastHit2D RCeiling = Physics2D.Raycast(topRight.position, topRight.up, rayDistance);

        // creates right raycast
        RaycastHit2D Tright = Physics2D.Raycast(rightTop.position, rightTop.right, rayDistance);
        RaycastHit2D Bright = Physics2D.Raycast(rightBottom.position, rightBottom.right, rayDistance);

        // creates left raycast
        RaycastHit2D Tleft = Physics2D.Raycast(leftTop.position, -(leftTop.right), rayDistance);
        RaycastHit2D Bleft = Physics2D.Raycast(leftBottom.position, -(leftBottom.right), rayDistance);

        collisionLeft = (Tleft || Bleft);
        collisionUp = (LCeiling || RCeiling);
        collisionDown = (LFloorRay || RFloorRay);
        collisionRight = (Tright || Bright);

    }
}
