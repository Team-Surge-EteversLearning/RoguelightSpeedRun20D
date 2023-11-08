using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState_Damage : State
{
    private Animator animator;
    private float timeCheck;

    public PlayerState_Damage()
    {
        stateName = "Damage";
        cooltime = 0.2f;
    }

    public override void Initialize(GameObject managerObject)
    {
        animator = managerObject.GetComponent<Animator>();
    }

    protected override string StateEnter_()
    {
        timeCheck = cooltime;
        animator.Play("Null");
        animator.Play(stateName, 0);
        animator.Play(stateName, 1);
        return "";
    }

    public override string StateUpdate()
    {
        timeCheck -= Time.deltaTime;

        if (timeCheck < 0)
            return "Idle";

        return "";
    }

    protected override void StateEnd()
    {
        
    }

    public override void Interrupt(GameObject managerObject)
    {

    }
}
