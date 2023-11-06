using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDamageState : State
{
    private MonsterSM stateManager;
    private int hpNow;
    public int damage = 0;

    public MonsterDamageState()
    {
        stateName = "Damage";
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
        hpNow = stateManager.basicData.hpMax;
    }

    protected override string StateEnter_()
    {
        if (damage > 0)
            hpNow -= damage;

        if (hpNow < 0)
            hpNow = 0;
        else
            return "Death";

        return "";
    }

    public override string StateUpdate()
    {
        return "";
    }

    protected override void StateEnd()
    {

    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}