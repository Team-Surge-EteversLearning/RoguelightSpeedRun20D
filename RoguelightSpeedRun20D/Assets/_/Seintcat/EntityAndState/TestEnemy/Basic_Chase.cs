using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Chase : State
{
    private MonsterSM stateManager;

    public Basic_Chase()
    {
        stateName = "Chase";
        cooltime = -1f;
        motionSpeed = 1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
    }

    protected override string StateEnter_()
    {
        stateManager.animator.Play(stateName);
        return "";
    }

    public override string StateUpdate()
    {
        if(stateManager.attackTarget == null)
            return "Idle";

        Vector3 targetPoint = stateManager.targetPos;
        if (!stateManager.basicData.isFly)
            targetPoint.y = stateManager.transform.position.y;

        stateManager.transform.LookAt(targetPoint);

        if (Vector3.Distance(stateManager.transform.position, targetPoint) < stateManager.basicData.attackRange)
            return "Attack";

        return "";
    }

    protected override void StateEnd()
    {
    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}
