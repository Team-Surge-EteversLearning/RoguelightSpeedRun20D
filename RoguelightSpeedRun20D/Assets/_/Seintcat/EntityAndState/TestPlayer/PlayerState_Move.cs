using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : State
{
    private Animator animator;
    private Transform transform;

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
        //animator
        return "";
    }

    public override string StateUpdate()
    {
        if (InputHandler.move == Vector2.zero)
        {
            return "Idle";
        }

        Vector3 normalMove = new Vector3(InputHandler.move.x, transform.position.y, InputHandler.move.y).normalized;

        transform.Translate(normalMove * Time.deltaTime);
        return "";
    }

    public override void Interrupt(GameObject managerObject)
    {

    }

    protected override void StateEnd()
    {

    }
}
