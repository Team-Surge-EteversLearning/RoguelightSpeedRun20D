using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    State nowState;
    Dictionary<string, Collider> attackParts;

    public virtual void ManagerStart()
    {

    }
    public virtual void ManagerUpdate()
    {

    }

    public abstract void Interrupt(string stateName);
}
