using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// @author Alex
/// </summary>
public class frameRateCounter : MonoBehaviour
{
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;

    private TextMeshProUGUI frameCount;

    private void Awake()
    {
        frameCount = gameObject.GetComponent<TextMeshProUGUI>();
        frameDeltaTimeArray = new float[50];
    }

    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

        frameCount.text = "FPS: " + Mathf.RoundToInt(calculateFPS()).ToString();
    }

    /// <summary>
    /// calculates the given FPS at a particular point
    /// </summary>
    /// <returns></returns>
    private float calculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }
}
