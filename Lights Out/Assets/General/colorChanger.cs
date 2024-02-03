using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// @author Alex
/// </summary>
public class colorChanger : MonoBehaviour
{

    private SpriteRenderer ren;
    private Light2D light2;

    // Start is called before the first frame update
    void Start()
    {
        ren = gameObject.GetComponent<SpriteRenderer>();
        light2 = gameObject.GetComponent<Light2D>();
    }

    public void changeRed(float red)
    {
        ren.color = new Color(red, ren.color.g, ren.color.b);
        light2.color = new Color(red, light2.color.g, light2.color.b);
    }


    public void changeGreen(float green)
    {
        ren.color = new Color(ren.color.r, green, ren.color.b);
        light2.color = new Color(light2.color.r, green, light2.color.b);
    }

    public void changeBlue(float blue)
    {
        ren.color = new Color(ren.color.r, ren.color.g, blue);
        light2.color = new Color(light2.color.r, light2.color.g, blue);
    }
}
