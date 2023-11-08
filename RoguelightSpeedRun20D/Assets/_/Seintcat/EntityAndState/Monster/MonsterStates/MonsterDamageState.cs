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
        stateManager.animator.Play(stateName);
        stateManager.transform.LookAt(PlayerSM.playerObj.transform.position);
        return "";
    }

    public override string StateUpdate()
    {
        cooltime -= Time.deltaTime;
        if(cooltime < 0f)
            return stateManager.attackTarget != null ? "Chase" : "Idle";

        if (stateManager.attackTarget != null)
        {
            Vector3 targetPoint = stateManager.attackTarget.transform.position;
            if (!stateManager.basicData.isFly)
                targetPoint.y = stateManager.transform.position.y;

            stateManager.transform.LookAt(targetPoint);
        }

        return "";
    }

    protected override void StateEnd()
    {

    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}