using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles camera movement such as following the player, going to different locations, zooming in and out, etc...
/// </summary>
public class CameraController : MonoBehaviour
{
    private const string cameraTag = "MainCamera";
    private Camera cam;

    public void Start()
    {
        GameObject camObject = GameObject.FindWithTag(cameraTag);

        if (camObject == null)
        {
            Debug.LogError("Can't find any element with tag " + cameraTag);
        }

        cam = camObject.GetComponent<Camera>();

        if (cam == null)
        {
            Debug.LogError("Found Element with camera tag " + cameraTag + " but there does not seem to be any attached camera object");
        }
    }

    public void Update()
    {
        if (cam == null) return;

        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
    }
}
