using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyAbleMonsterDamageState : MonsterDamageState
{
    public CopyAbleMonsterDamageState()
    {
        cooltime = -1f;
        motionSpeed = 1f;
    }

    protected override string StateEnter_()
    {
        // get Damage
        string returnValue = base.StateEnter_();

        if(returnValue == "Death")
            return returnValue;

        return "";
    }

    public override string StateUpdate()
    {
        return "";
    }

    protected override void StateEnd()
    {

    }
}
