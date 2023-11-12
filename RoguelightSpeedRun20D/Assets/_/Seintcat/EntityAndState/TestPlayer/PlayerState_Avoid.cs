using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Avoid : State
{
    private PlayerSM stateManager;
    private Vector3 normalMove;
    private Rigidbody rigidBody;
    private Animator animator;

    public PlayerState_Avoid()
    {
        stateName = "Avoid";
        cooltime = -1f;
        motionSpeed = 0.7f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<PlayerSM>();
        rigidBody = managerObject.GetComponent<Rigidbody>();
        animator = managerObject.GetComponent<Animator>();
    }

    protected override string StateEnter_()
    {
        normalMove = new Vector3(InputHandler.move.x, 0, InputHandler.move.y).normalized;
        if(normalMove == Vector3.zero)
            return "Move";

        PlayerSM.staminaNow -= 3f;
        animator.SetFloat("X", Mathf.Lerp(animator.GetFloat("X"), normalMove.x, Time.deltaTime * 2));
        animator.SetFloat("Z", Mathf.Lerp(animator.GetFloat("Z"), normalMove.z, Time.deltaTime * 2));

        normalMove = Quaternion.Euler(0, rigidBody.transform.rotation.eulerAngles.y, 0) * normalMove;

        animator.Play(stateName, 0);
        animator.Play(stateName, 1);
        cooltime = motionSpeed;
        return "";
    }

    public override string StateUpdate()
    {
        float speed = PlayerSM.moveSpeed;
        speed += ((float)(PlayerStatsManager.Speed + PlayerSM.shoesNow.Speed + 10f) / (PlayerStatsManager.Speed + 10)) * PlayerSM.speedMaxGap;
        rigidBody.AddForce(normalMove * speed, ForceMode.VelocityChange);

        cooltime -= Time.deltaTime;
        if(cooltime < 0)
            return "Move";

        return "";
    }

    protected override void StateEnd()
    {
    }

    public override void Interrupt(GameObject managerObject)
    {
    }
}
