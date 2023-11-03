using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Idle : State
{
    private Animator animator;

    public PlayerState_Idle()
    {
        stateName = "Idle";
        cooltime = -1f;
        motionSpeed = 1f;
    }
    public override void Initialize(GameObject managerObject)
    {
        animator = managerObject.GetComponent<Animator>();
    }

    protected override string StateEnter_()
    {
        animator.Play(stateName);
        return "";
    }

    public override string StateUpdate()
    {
        if(InputHandler.move != Vector2.zero)
        {
            return "Move";
        }
        return "";
    }

    public override void Interrupt(GameObject managerObject)
    {

    }

    protected override void StateEnd()
    {

    }
}
