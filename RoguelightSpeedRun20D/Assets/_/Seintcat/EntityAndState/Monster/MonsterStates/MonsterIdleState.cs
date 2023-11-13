using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : State
{
    private MonsterSM stateManager;
    private Quaternion startRotation;

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
        startRotation = stateManager.transform.rotation;
        stateManager.animator.Play(stateName, 0);
        stateManager.animator.Play(stateName, 1);
        cooltime = stateManager.basicData.idleTime;
        return "";
    }

    public override string StateUpdate()
    {
        if(stateManager.movePoints.Count < 1)
            return "";

        cooltime -= Time.deltaTime;
        if (stateManager.basicData.idleTime > 0f && cooltime < 0f)
            return "Patrol";

        Vector3 targetPoint = stateManager.targetPos;
        if (!stateManager.basicData.isFly)
            targetPoint.y = stateManager.transform.position.y;

        stateManager.transform.rotation = Quaternion.Lerp
            (startRotation,
            Quaternion.LookRotation(targetPoint - stateManager.transform.position, Vector3.up),
            (stateManager.basicData.idleTime - cooltime) / stateManager.basicData.idleTime);

        return "";
    }

    protected override void StateEnd()
    {
    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}