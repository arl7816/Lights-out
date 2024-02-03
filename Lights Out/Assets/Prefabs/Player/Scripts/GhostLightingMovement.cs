using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// @author Alex
/// </summary>
public class GhostLightingMovement : MonoBehaviour
{

    public Slider slider;
    private float health = 100f;
    private bool inLight = true;

    [SerializeField]
    public bool godMode = false;

    [SerializeField]
    private float damage = 1f;

    [SerializeField]
    private float damagePerSecond = 4;

    private void Start()
    {
        StartCoroutine(takeDamnge());
    }

    public void heal(float healAmount)
    {
        health = (health + healAmount > 100) ? 100 : health + healAmount;
        slider.value = health / 100;
    }

    IEnumerator takeDamnge()
    {
        if (!inLight && !godMode)
        {
            health -= damage;

            slider.value = health / 100;

            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        yield return new WaitForSeconds(1/damagePerSecond);

        StartCoroutine(takeDamnge());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            inLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            inLight = false;
        }
    }
}
