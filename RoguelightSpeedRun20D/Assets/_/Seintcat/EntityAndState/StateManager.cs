using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    protected State nowState;
    protected Dictionary<string, Collider> attackParts;
    protected Dictionary<string, State> allStates;

    public virtual void ManagerStart()
    {
        MakeState();

        foreach (State state in allStates.Values)
            state.Initialize(gameObject);
    }
    public virtual void ManagerUpdate()
    {
        string nextState = "";
        if (nowState.started)
        {
            nextState = nowState.StateUpdate();
        }
        else
        {
            do
            {
                nowState.StateEnd_();
                nextState = nowState.StateEnter();
            } while (nextState != "");
            nextState = nowState.StateUpdate();
        }

        if (nextState != "")
        {
            nowState.StateEnd_();
            nowState = allStates[nextState];
        }
    }

    public abstract void MakeState();

    public abstract void Interrupt(string stateName);
}
