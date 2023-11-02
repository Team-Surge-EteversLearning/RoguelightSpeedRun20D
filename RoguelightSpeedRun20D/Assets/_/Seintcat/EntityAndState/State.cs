using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    private readonly string stateName;
    private readonly float cooltime;
    private readonly float motionSpeed;

    public abstract State StateEnter(GameObject managerObject);
    public abstract State StateUpdate(GameObject managerObject);
    public abstract void StateEnd(GameObject managerObject);
    public abstract void Interrupt(GameObject managerObject);
}
