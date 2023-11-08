using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasic_Chase : State
{
    private MonsterSM stateManager;
    private Rigidbody rigidbody;

    public MonsterBasic_Chase()
    {
        stateName = "Chase";
        cooltime = -1f;
        motionSpeed = 1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
        rigidbody = managerObject.GetComponent<Rigidbody>();
    }

    protected override string StateEnter_()
    {
        if (stateManager.attackTarget != null)
        {
            Vector3 targetPoint = stateManager.attackTarget.transform.position;
            if (!stateManager.basicData.isFly)
                targetPoint.y = stateManager.transform.position.y;

            if (Vector3.Distance(stateManager.transform.position, targetPoint) < stateManager.basicData.attackRange)
                return "Attack";
        }
        stateManager.animator.Play(stateName);
        return "";
    }

    public override string StateUpdate()
    {
        if(stateManager.attackTarget == null)
            return "Idle";

        Vector3 targetPoint = stateManager.attackTarget.transform.position;
        if (!stateManager.basicData.isFly)
            targetPoint.y = stateManager.transform.position.y;

        stateManager.transform.LookAt(targetPoint);

        if (Vector3.Distance(stateManager.transform.position, targetPoint) < stateManager.basicData.attackRange)
            return "Attack";

        return "";
    }

    protected override void StateEnd()
    {
        rigidbody.velocity = Vector3.zero;
    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}
