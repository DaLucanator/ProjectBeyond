using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        GameEvents.current = this;
    }

    public event Action<int> PowerOn;
    public void PowerOnMethod(int id)
    {
        if(PowerOn != null) { PowerOn(id); }
    }

    public event Action<int> PowerOff;
    public void PowerOffMethod(int id)
    {
        if (PowerOff != null) { PowerOff(id); }
    }

    public event Action<float> SetHorizontalMovement;
    public void SetHorizontalMovementMethod (float direction)
    {
        if (SetHorizontalMovement != null) { SetHorizontalMovement(direction); }
    }
}
