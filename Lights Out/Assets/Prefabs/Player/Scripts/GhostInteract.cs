using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// @author Alex
/// </summary>
public class GhostInteract : MonoBehaviour
{
    [SerializeField]
    private float circleRadius = 1;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomMulitply = 3f;

    [SerializeField]
    private float distancePerFrame = 10f;

    private float camStartSize;
    private float desiredCamSize;
    
    private enum Zooming{
        ZOOMIN,
        ZOOMOUT,
        NOZOOM
    }

    private Zooming iszooming = Zooming.NOZOOM;

    private void Awake()
    { 
        camStartSize = cam.orthographicSize;
        desiredCamSize = camStartSize;
    }

    /// <summary>
    /// checks if the player if the player is near a type of object when they attempt to interact with it
    /// </summary>
    private void checkInteract()
    {
        LayerMask mask = 6;
        Collider2D[] colls = Physics2D.OverlapCircleAll(gameObject.transform.position, circleRadius);
        foreach (Collider2D coll in colls){
            switch (coll.tag)
            {
                case "Lever":
                    coll.gameObject.GetComponent<LeverScript>().flip();
                    break;
            }
        }
    }


    // this needs to be generalized before release
    private void Update()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown("e"))
        {
            checkInteract();
        }

        if (Input.GetKeyDown("z"))
        {
            desiredCamSize = camStartSize * zoomMulitply;
            iszooming = Zooming.ZOOMOUT;
        }

        if (Input.GetKeyUp("z"))
        {
            desiredCamSize = camStartSize;
            iszooming = Zooming.ZOOMIN;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (cam.orthographicSize != desiredCamSize && (iszooming != Zooming.NOZOOM))
        {
            float change = cam.orthographicSize < desiredCamSize ? distancePerFrame: -distancePerFrame;
            cam.orthographicSize += change * Time.deltaTime;

            if (iszooming == Zooming.ZOOMIN)
            {
                if (cam.orthographicSize <= desiredCamSize)
                {
                    iszooming = Zooming.NOZOOM;
                }
            }
            else // zoomout
            {
                if (cam.orthographicSize >= desiredCamSize)
                {
                    iszooming = Zooming.NOZOOM;
                }
            }
        }
    }
}
