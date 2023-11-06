using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulState_Move : State
{
    private GhoulSM ghoul;

    public GhoulState_Move()
    {
        stateName = "Move";
        cooltime = -1f;
        motionSpeed = 1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        ghoul = managerObject.GetComponent<GhoulSM>();
    }

    protected override string StateEnter_()
    {
        ghoul.headWalk.enabled = true;
        return "";
    }

    public override string StateUpdate()
    {
        if (!ghoul.foundPlayer)
            return "Idle";



        return "";
    }

    protected override void StateEnd()
    {
        ghoul.headWalk.enabled = false;
    }

    public override void Interrupt(GameObject managerObject)
    {

    }
}
