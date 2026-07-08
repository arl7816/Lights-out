using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jumpscare : MonoBehaviour
{
    public AudioSource jumpscareSound;
    public Image image;
    // Start is called before the first frame update

    void Start()
    {
        image.enabled = false;
    }
    public void Jumpscared()
    {
        jumpscareSound.Play();
        image.enabled = true;
        Invoke("DisableImage", 0.5f);
    }

    private void DisableImage()
    {
         image.enabled = false;
    }
}
