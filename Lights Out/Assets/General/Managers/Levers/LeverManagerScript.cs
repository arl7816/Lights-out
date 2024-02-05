using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventTrigger : UnityEvent { }

// @author Alex
public class LeverManagerScript : MonoBehaviour
{
    [SerializeField]
    private string managerName = "Default Name";

    /// <summary>
    /// A Lever can either be up or down
    /// </summary>
    [System.Serializable]
    public class Lever
    {
        public GameObject lever;
        public bool state;
    }
    public Lever[] levers;


    public EventTrigger activate;
    public EventTrigger deactivate;

    /// <summary>
    /// Flips a lever, in the event all levers are flipped, an event is activated, otherwise
    /// deactivated
    /// </summary>
    /// <param name="lever">The lever being flipped</param>
    public void leverFlip(GameObject lever)
    {
        bool allTrue = true;
        bool foundObject = false;


        for (int i = 0; i < levers.Length; i++)
        {
            if (levers[i].lever == lever)
            {
                foundObject = true;
            }

            if (levers[i].lever.GetComponent<LeverScript>().state != levers[i].state) // if the state is wrong set allTrue to false
            {
                allTrue = false;
            }

            if (!allTrue && foundObject)
            {
                break;
            }

        }

        if (allTrue)
        {
            activated();
        }

        if (!allTrue && deactivate != null)
        {
            deactivated();
        }
    }

    public void activated()
    {
        activate.Invoke();
    }

    public void deactivated()
    {
        deactivate.Invoke();
    }
}