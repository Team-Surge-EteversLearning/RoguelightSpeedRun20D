using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    protected State mainState;
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
        if (mainState.started)
        {
            nextState = mainState.StateUpdate();
        }
        else
        {
            do
            {
                mainState.StateEnd_();
                nextState = mainState.StateEnter();
            } while (nextState != "");
            nextState = mainState.StateUpdate();
        }

        if (nextState != "")
        {
            mainState.StateEnd_();
            mainState = allStates[nextState];
        }
    }

    public abstract void MakeState();

    public abstract void Interrupt(string stateName);
}
