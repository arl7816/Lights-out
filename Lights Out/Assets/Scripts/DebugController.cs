using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugController: MonoBehaviour
{
    private bool showConsole = false;
    private string input;
    private bool showHelp;

    private Vector2 scroll;

    public static DebugCommand HEALTH_MAX;
    public static DebugCommand<int> GIVE_HEALTH;
    public static DebugCommand HELP;
    public static DebugCommand REMOVE_HELP;
    public static DebugCommand GOD_MODE;
    public static DebugCommand RESET;
    public static DebugCommand HOME;
    public static DebugCommand FREE;
    public static DebugCommand<int> SET_SPEED;


    public List<object> commandList;

    public GhostLightingMovement ghostLightingMovement;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        HEALTH_MAX = new DebugCommand("health_max", "Fully heals you", "health_max", () =>
        {
            ghostLightingMovement.heal(1000);
        });

        GIVE_HEALTH = new DebugCommand<int>("give_health", "Gives you n health", "give_health (n)", (n) =>
        {
            ghostLightingMovement.heal(n);
        });

        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = true;
        });

        REMOVE_HELP = new DebugCommand("help-r", "Removes the list of command given from help", "help-r", () => 
        {
            showHelp = false;
        });

        GOD_MODE = new DebugCommand("god", "Enters god mode or leaves it", "god", () => {
            ghostLightingMovement.godMode = !ghostLightingMovement.godMode;
        });

        RESET = new DebugCommand("reset", "Resets the current level", "reset", () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        HOME = new DebugCommand("home", "goes to home screen", "home", () =>
        {
            SceneManager.LoadScene(0);
        });

        FREE = new DebugCommand("free", "makes the player go through walls and float, use again to disable", "free", () =>
        {
            playerMovement.changeMoveMode();
        });

        SET_SPEED = new DebugCommand<int>("set_speed", "Sets the players speed to n", "set_speed (n)", (n) =>
        {
            playerMovement.setSpeed(n);
        });

        commandList = new List<object> {
            HEALTH_MAX,
            GIVE_HEALTH,
            HELP,
            REMOVE_HELP,
            GOD_MODE,
            RESET,
            HOME,
            FREE,
            SET_SPEED
        };
    }

    void OnReturn()
    {
        if (showConsole) 
        {
            HandleInput();
            input = "";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("/") || Input.GetKeyDown("`")) {
            showConsole = !showConsole;

            if (!showConsole) { showHelp = false; }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnReturn();
        }

    }

    private void OnGUI()
    {
        if (!showConsole) { return; }

        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, 0, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100;

        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, y, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

        
    }

    private void HandleInput() 
    {
        string[] properties = input.Split(' ');

        for (int i = 0; i < commandList.Count; i++) 
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandId)) 
            {
                showHelp = false;
                if (properties.Length == 1)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}
