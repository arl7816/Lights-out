using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @author Alex

public class LeverScript : MonoBehaviour
{

    public bool state = true; // down is false, up is true
    private LeverManagerScript[] leverManager;
    private Animator ani;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        switchDir();

        leverManager = GameObject.Find("GameManager").GetComponentsInChildren<LeverManagerScript>();
    }

    /// <summary>
    /// Flips a lever and lets the lever manager aware of the flip
    /// </summary>
    public void flip()
    {
        state = !state;
        switchDir();

        for (int i = 0; i < leverManager.Length; i++)
        {
            leverManager[i].leverFlip(gameObject);
        }
    }

    /// <summary>
    /// Changes the appearnce of the lever depending on the direction of 
    /// a flip
    /// </summary>
    private void switchDir()
    {
        if (state)
        {
            sr.color = new Color32(0, 255, 68, 255);
        }
        else
        {
            sr.color = new Color32(255, 255, 255, 255);
        }
    }
}
