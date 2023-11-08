using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathState : State
{
    private MonsterSM stateManager;

    public MonsterDeathState()
    {
        stateName = "Death";
        cooltime = -1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
    }

    protected override string StateEnter_()
    {
        stateManager.animator.Play(stateName);
        cooltime = stateManager.basicData.deadTime;
        return "";
    }

    public override string StateUpdate()
    {
        cooltime -= Time.deltaTime;
        if(cooltime < 0)
            stateManager.gameObject.SetActive(false);

        return "";
    }

    protected override void StateEnd()
    {
    }

    public override void Interrupt(GameObject managerObject)
    {

    }
}