using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : State
{
    private Animator animator;
    private Rigidbody rigidBody;

    private string runCheck => InputHandler.running ? "Run" : "";

    public PlayerState_Move()
    {
        stateName = "Move";
        cooltime = -1f;
        motionSpeed = 1f;
    }
    public override void Initialize(GameObject managerObject)
    {
        animator = managerObject.GetComponent<Animator>();
        rigidBody = managerObject.GetComponent<Rigidbody>();
    }

    protected override string StateEnter_()
    {
        animator.Play(stateName + runCheck, 0);
        return "";
    }

    public override string StateUpdate()
    {
        if (InputHandler.move == Vector2.zero)
        {
            return "Idle";
        }

        bool runNow = false;
        float speed = PlayerSM.moveSpeed;
        if (!PlayerSM.isDefence && InputHandler.running && PlayerSM.staminaNow > 0)
        {
            runNow = true;
            speed = PlayerSM.runSpeed;
            PlayerSM.staminaNow -= Time.deltaTime;
        }

        Vector3 normalMove = new Vector3(InputHandler.move.x, 0, InputHandler.move.y).normalized;
        animator.SetFloat("X", Mathf.Lerp(animator.GetFloat("X"), normalMove.x, Time.deltaTime * 2));
        animator.SetFloat("Z", Mathf.Lerp(animator.GetFloat("Z"), normalMove.z, Time.deltaTime * 2));
        animator.SetBool("Running", runNow);

        speed += ((float)(PlayerStatsManager.Speed + PlayerSM.shoesNow.Speed) / (PlayerStatsManager.Speed + 10)) * PlayerSM.speedMaxGap;
        rigidBody.AddForce(normalMove * speed, ForceMode.VelocityChange);

        return "";
    }

    public override void Interrupt(GameObject managerObject)
    {

    }

    protected override void StateEnd()
    {
        rigidBody.velocity = Vector3.zero;
        //Debug.LogWarning("!");
        animator.Play("Idle" + runCheck, 0);
    }
}
