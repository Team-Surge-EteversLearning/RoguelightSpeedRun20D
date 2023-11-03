using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : State
{
    private Animator animator;
    private Transform transform;

    private string runCheck => InputHandler.running? "Run" : "";

    public PlayerState_Move()
    {
        stateName = "Move";
        cooltime = -1f;
        motionSpeed = 1f;
    }
    public override void Initialize(GameObject managerObject)
    {
        animator = managerObject.GetComponent<Animator>();
        transform = managerObject.transform;
    }

    protected override string StateEnter_()
    {
        animator.Play(stateName + runCheck);
        return "";
    }

    public override string StateUpdate()
    {
        if (InputHandler.move == Vector2.zero)
        {
            return "Idle";
        }

        Vector3 normalMove = new Vector3(InputHandler.move.x, transform.position.y, InputHandler.move.y).normalized;
        animator.SetFloat("X", Mathf.Lerp(animator.GetFloat("X"), normalMove.x, Time.deltaTime * 2));
        animator.SetFloat("Z", Mathf.Lerp(animator.GetFloat("Z"), normalMove.z, Time.deltaTime * 2));
        transform.Translate(normalMove * Time.deltaTime);
        return "";
    }

    public override void Interrupt(GameObject managerObject)
    {

    }

    protected override void StateEnd()
    {
        animator.Play("Idle" + runCheck);
    }
}
