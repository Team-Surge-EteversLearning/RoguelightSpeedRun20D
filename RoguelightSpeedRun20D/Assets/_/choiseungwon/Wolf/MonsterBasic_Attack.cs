using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasic_Attack : State
{
    private MonsterSM stateManager;
    private Rigidbody rigidbody;
    public MonsterBasic_Attack()
    {
        stateName = "Attack";
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
        throw new System.NotImplementedException();
    }

    public override string StateUpdate()
    {
        throw new System.NotImplementedException();
    }

    protected override void StateEnd()
    {
        throw new System.NotImplementedException();
    }

    public override void Interrupt(GameObject managerObject)
    {
        throw new System.NotImplementedException();
    }
}
