using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolState : State
{
    private MonsterSM stateManager;
    private Transform moveTarget;
    private Rigidbody rigidBody;

    public MonsterPatrolState()
    {
        stateName = "Patrol";
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
        rigidBody = managerObject.GetComponent<Rigidbody>();
    }

    protected override string StateEnter_()
    {
        if(stateManager.movePoints.Count < 1)
            return "Idle";

        moveTarget = stateManager.NextPatrol();

        stateManager.animator.Play(stateName);
        return "";  
    }

    public override string StateUpdate()
    {
        Vector3 targetPoint = moveTarget.position;
        if (!stateManager.basicData.isFly)
            targetPoint.y = stateManager.transform.position.y;

        if (Vector3.Distance(stateManager.transform.position, targetPoint) < 0.05f)
        {
            moveTarget = stateManager.NextPatrol();
            if (stateManager.basicData.idleTime > 0)
                return "Idle";
        }

        stateManager.transform.LookAt(targetPoint);

        return "";
    }

    protected override void StateEnd()
    {
        rigidBody.velocity = Vector3.zero;
    }

    public override void Interrupt(GameObject managerObject)
    {

    }
}
