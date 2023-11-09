using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Don't edit this, Copy this!
public class MonsterBasic_Range : State
{
    private MonsterSM stateManager;


    private AttackAble projectile; // stateManager에 정의
    private Transform launchPosition; // stateManager에 정의
    private float projectileSpeed; //projectile에 붙은 Projectile_Attackable에 정의
    public MonsterBasic_Range()
    {
        stateName = "Attack";
        cooltime = -1f;
        motionSpeed = 1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
        projectile = stateManager.attackAbles[0];
        //launchPosition = stateManager.attackAbles[1].transform;
    }

    protected override string StateEnter_()
    {
        if (stateManager.attackTarget != null)
        {
            Vector3 targetPoint = stateManager.attackTarget.transform.position;
            if (!stateManager.basicData.isFly)
                targetPoint.y = stateManager.transform.position.y;

            stateManager.transform.LookAt(targetPoint);
        }
        cooltime = stateManager.basicData.attackCoolTime;
        stateManager.animator.Play(stateName);
        GameObject instance  = GameObject.Instantiate(projectile.gameObject, launchPosition);
        instance.GetComponent<AttackAble>().AttackStart();
        return "";
    }

    public override string StateUpdate()
    {
        cooltime -= Time.deltaTime;

        if (cooltime < 0f)
            return "Chase";

        if (stateManager.attackTarget != null)
        {
            Vector3 targetPoint = stateManager.attackTarget.transform.position;
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
