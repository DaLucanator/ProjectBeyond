using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Gadget
{
    public GameObject[] connections;
    public GameObject gameobject;
    [ReadOnly] [SerializeField] private bool buttonCheck, buttonOn;

    private void Start()
    {
        gadgetName = "Button";
        assignTag(gadgetName);

        buttonCheck = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            if (buttonCheck)
            {
                Debug.Log("2");
                ButtonPress(true);
                buttonCheck = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            ButtonPress(false);
            buttonCheck = true;
        }
    }

    private void ButtonPress(bool on)
    {
        Debug.Log("3");
        if (connections.Length > 0)
        {
            Debug.Log("4");
            foreach (GameObject connection in connections)
            {
                Debug.Log(connection.GetInstanceID());
                if (on) { GameEvents.current.PowerOnMethod(connection.GetInstanceID()); }
                if (!on) { GameEvents.current.PowerOffMethod(connection.GetInstanceID()); }
            }
        }
    }
}
