using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : MonoBehaviour
{
    [ReadOnly] [SerializeField] protected string gadgetName;

    protected void assignTag(string name)
    {
        gameObject.tag = name;
    }
}

public class PoweredGadget: Gadget
{
    [SerializeField] protected bool isAlwaysPowered;
    [ReadOnly] [SerializeField] protected bool isPowered;
    public Material material;

    protected  virtual void Start()
    {
        GameEvents.current.PowerOn += PowerOn;
        GameEvents.current.PowerOff += PowerOff;
        material = gameObject.GetComponent<Renderer>().material;
        if (isAlwaysPowered) { isPowered = true; }
        if (isPowered) { PowerOn(gameObject.GetInstanceID()); }
        if (!isPowered) { PowerOff(gameObject.GetInstanceID()); }
    }

    private void PowerOn(int id)
    {
        if (id == gameObject.GetInstanceID()) 
        { 
            isPowered = true;
            material.SetColor("_Color", Color.green);
        }
    }

    private void PowerOff(int id)
    {
        if(id == gameObject.GetInstanceID() && !isAlwaysPowered)
        {
            isPowered = false;
            material.SetColor("_Color", Color.red);
        }
    }
}


