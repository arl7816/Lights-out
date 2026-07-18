using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMiddleScrip : MonoBehaviour
{

    private const int COLLISION_LAYER = 3;
    private const string LOGIC_TAG = "Logic";

    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag(LOGIC_TAG).GetComponent<LogicScript>();
        Debug.Log("name is: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == COLLISION_LAYER)
        {
             logic.addScore(1);
        }
       
    }
}
