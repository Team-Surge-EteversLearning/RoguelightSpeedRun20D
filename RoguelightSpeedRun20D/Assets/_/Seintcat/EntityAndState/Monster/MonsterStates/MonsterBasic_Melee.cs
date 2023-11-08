using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasic_Melee : State
{
    [SerializeField]
    private MonsterSM stateManager;
    private float motionStop;

    public MonsterBasic_Melee()
    {
        stateName = "Attack";
        cooltime = -1f;
        motionSpeed = 1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
    }

    protected override string StateEnter_()
    {
        //if (stateManager.attackTarget != null)
        //{
        //    Vector3 targetPoint = stateManager.attackTarget.transform.position;
        //    if (!stateManager.basicData.isFly)
        //        targetPoint.y = stateManager.transform.position.y;

        //    stateManager.transform.LookAt(targetPoint);
        //}

        cooltime = stateManager.basicData.attackCoolTime;
        motionStop = stateManager.basicData.motionTime;
        stateManager.animator.Play(stateName);
        foreach(AttackAble attackAble in stateManager.attackAbles)
            attackAble.AttackStart();
        return "";
    }

    public override string StateUpdate()
    {
        cooltime -= Time.deltaTime;
        motionStop -= Time.deltaTime;

        if (motionStop < 0f)
            foreach (AttackAble attackAble in stateManager.attackAbles)
                attackAble.AttackStop();

        if (cooltime < 0f)
            return "Chase";

        if (stateManager.attackTarget != null)
        {
            Vector3 targetPoint = stateManager.targetPos;
            if (!stateManager.basicData.isFly)
                targetPoint.y = stateManager.transform.position.y;

            stateManager.transform.rotation = Quaternion.Lerp
                (stateManager.transform.rotation,
                Quaternion.LookRotation(targetPoint - stateManager.transform.position, Vector3.up),
                (stateManager.basicData.idleTime - cooltime) / stateManager.basicData.idleTime);
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
