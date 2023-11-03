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
        return "";
    }

    public override string StateUpdate()
    {
        if(InputHandler.move != Vector2.zero)
        {
            return "Move";
        }
        animator.SetFloat("X", Mathf.Lerp(animator.GetFloat("X"), 0, Time.deltaTime * 4));
        animator.SetFloat("Z", Mathf.Lerp(animator.GetFloat("Z"), 0, Time.deltaTime * 4));
        return "";
    }

    public override void Interrupt(GameObject managerObject)
    {

    }

    protected override void StateEnd()
    {

    }
}
