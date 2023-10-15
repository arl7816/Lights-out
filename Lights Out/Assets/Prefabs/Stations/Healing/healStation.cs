using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healStation : MonoBehaviour
{
    GameObject player;

    [SerializeField]
    private float healPerSecond = 1f;

    [SerializeField]
    private float healingAmount = 1f;

    private bool healing = false;

    IEnumerator healPlayer()
    {
        if (healing)
        {
            player.GetComponent<GhostLightingMovement>().heal(healingAmount);
        }

        yield return new WaitForSeconds(1 / healPerSecond);

        StartCoroutine(healPlayer());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            healing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            healing = true;
            StartCoroutine(healPlayer());
        }
    }
}
