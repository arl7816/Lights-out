using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// @author Dominic --DON'T TOUCH UNLESS **SPECIFIC** INSTRUCTIONS TO--

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;

    /// <summary>
    /// Goes to the Next Scene
    /// </summary>
    public void OpenScene()
    {
        //SceneManager.LoadScene("Level0." + level.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
