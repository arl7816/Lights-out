using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Alex 
/// </summary>
public class HealCrystal : MonoBehaviour
{
    [SerializeField]
    private float healAmount = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // player layer
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<GhostLightingMovement>().heal(healAmount);

            Destroy(gameObject);
        }
    }
}
