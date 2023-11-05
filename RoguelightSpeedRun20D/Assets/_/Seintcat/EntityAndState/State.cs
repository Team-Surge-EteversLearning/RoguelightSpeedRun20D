using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public string stateName { get; protected set; }
    public bool started {  get; protected set; }

    protected float cooltime { get; set; }
    protected float motionSpeed { get; set; }

    public State() 
    {
        started = false;
    }

    public abstract void Initialize(GameObject managerObject);

    protected abstract string StateEnter_();
    public virtual string StateEnter()
    {
        started = true;
        Debug.Log(stateName);
        return StateEnter_();
    }
    public abstract string StateUpdate();
    protected abstract void StateEnd();
    public virtual void StateEnd_()
    {
        StateEnd();
        started = false;
    }
    public abstract void Interrupt(GameObject managerObject);
}
