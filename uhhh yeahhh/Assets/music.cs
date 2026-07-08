using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    public AudioSource backgroundSource;
    public AudioSource gameOverSource;
    public AudioSource jumpscareSource;
    public birdscript bird;
    private bool played = false;


    // Call this method when the player loses
    void Update()
    {
      if (!bird.birdIsAlive && !played)
        {
            backgroundSource.Stop();
        gameOverSource.Play();
        jumpscareSource.Play();
        played = true;
        }
       
    }
}