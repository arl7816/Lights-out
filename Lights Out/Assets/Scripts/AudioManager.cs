using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// @author Dominic --DON'T TOUCH UNLESS **SPECIFIC** INSTRUCTIONS TO--

public class AudioManager : MonoBehaviour
{
    // 
    [SerializeField] Slider volumeSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        // if-else statement to determine whether to make new volume settings or to load
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    /// <summary>
    /// Simply changes the volume of the game with the slider
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    /// <summary>
    /// Loads whatever value the music setting is
    /// </summary>
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    /// <summary>
    /// Saves the volume slider settings
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
