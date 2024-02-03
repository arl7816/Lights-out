using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// @author Alex
/// </summary>
public class BlueCrystal : MonoBehaviour
{

    private bool hasCollided = false;

    private GameObject mainTorch;

    private void Start()
    {
        mainTorch = GameObject.Find("MainTorch");
        mainTorch.GetComponent<TorchScript>().addTorch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" || hasCollided){
            return;
        }

        hasCollided = true;

        mainTorch.GetComponent<TorchScript>().removeTorch();

        Destroy(gameObject);
    }
}
