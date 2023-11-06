using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulState_Idle : State
{
    private GhoulSM ghoul;

    public GhoulState_Idle()
    {
        stateName = "Idle";
        cooltime = -1f;
        motionSpeed = 1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        ghoul = managerObject.GetComponent<GhoulSM>();
    }

    protected override string StateEnter_()
    {
        ghoul.headIdle.enabled = true;
        return "";
    }

    public override string StateUpdate()
    {
        if (ghoul.foundPlayer)
            return "Move";

        return "";
    }

    protected override void StateEnd()
    {
        ghoul.headIdle.enabled = false;
    }

    public override void Interrupt(GameObject managerObject)
    {
        
    }
}
