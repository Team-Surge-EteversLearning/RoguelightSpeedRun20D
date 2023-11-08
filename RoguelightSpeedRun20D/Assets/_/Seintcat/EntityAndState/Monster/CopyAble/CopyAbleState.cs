using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Don't edit this, Copy this!
public class CopyAbleState : State
{
    public CopyAbleState()
    {
        stateName = "";
        cooltime = -1f;
        motionSpeed = 1f;
    }

    public override void Initialize(GameObject managerObject)
    {
    }

    protected override string StateEnter_()
    {
        return "";
    }

    public override string StateUpdate()
    {
        return "";
    }

    protected override void StateEnd()
    {
    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}
