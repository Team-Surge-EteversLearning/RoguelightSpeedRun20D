using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : State
{
    private MonsterSM stateManager;

    public MonsterIdleState()
    {
        stateName = "Idle";
        cooltime = -1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
    }

    protected override string StateEnter_()
    {
        stateManager.animator.Play(stateName);
        cooltime = stateManager.basicData.idleTime;
        return "";
    }

    public override string StateUpdate()
    {
        cooltime -= Time.deltaTime;
        if (cooltime < 0f)
            return "";

        return "";
    }

    protected override void StateEnd()
    {
    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}