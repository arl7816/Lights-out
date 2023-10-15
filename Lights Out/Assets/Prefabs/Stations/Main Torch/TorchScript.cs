using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TorchScript : MonoBehaviour
{
    private int torchAmount = 0;
    private int maxAmount = 0;
    bool haveSeenTorch = false; // prevents the torch from thinking everything is collected right at the start

    private new Light2D light;
    private Animator ani;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GameObject.Find("CrystalCounter").GetComponent<TextMeshProUGUI>();
        light = gameObject.GetComponentInChildren<Light2D>();
        ani = gameObject.GetComponent<Animator>();
    }

    private void updateText()
    {
        text.text = (maxAmount - torchAmount) + "/" + maxAmount;
    }

    public void addTorch()
    {
        haveSeenTorch = true;
        torchAmount++;

        if (torchAmount > maxAmount)
        {
            maxAmount = torchAmount;
            updateText();
        }
    }


    public void removeTorch()
    {
        torchAmount--;

        if (torchAmount < 0) { torchAmount = 0; }

        if (torchAmount <= 0)
        {
            ani.SetTrigger("blue_fire");
            light.color = new Color32(0, 255, 253, 255);
        }
        updateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && torchAmount <= 0 && haveSeenTorch)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
