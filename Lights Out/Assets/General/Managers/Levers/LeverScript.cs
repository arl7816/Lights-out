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

    public void flip()
    {
        state = !state;
        switchDir();

        for (int i = 0; i < leverManager.Length; i++)
        {
            leverManager[i].leverFlip(gameObject);
        }
    }


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
