using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDamageState : State
{
    private MonsterSM stateManager;

    public MonsterDamageState()
    {
        stateName = "Damage";
        cooltime = -1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
    }

    protected override string StateEnter_()
    {
        cooltime = stateManager.basicData.damagedStaggerTime;
        return "";
    }

    public override string StateUpdate()
    {
        cooltime -= Time.deltaTime;
        if(cooltime < 0f)
            return "Idle";

        return "";
    }

    protected override void StateEnd()
    {

    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}