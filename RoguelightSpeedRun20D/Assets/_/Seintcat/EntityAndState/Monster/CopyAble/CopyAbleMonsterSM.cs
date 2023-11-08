using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Don't edit this, Copy this!
public class CopyAbleMonsterSM : MonsterSM
{
    protected override List<State> monsterBattleStates => new List<State>();

    private void Awake()
    {
        ManagerStart();
    }

    public override void MakeState()
    {
        // make custom states

        // make basic states
        base.MakeState();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void _FixedUpdate()
    {

    }

    public override void TargetChanged(List<GameObject> target)
    {
        
    }

    public override void Interrupt(string stateName)
    {
        // not using
    }

    protected override void OnTriggerEnter(Collider other)
    {
        AttackAble otherAttack = other.GetComponent<AttackAble>();
        if (otherAttack != null)
        {
            GetDamage(otherAttack.GetDamage(gameObject), other);
        }
    }

    protected override void ReactDamage()
    {
        
    }

    protected override void ResetStateMachine()
    {
        
    }
}

/*
 * other code
 * ChangeState("name");
 * monsterIdleState.stateName = "Idle"
 * monsterDamageState.stateName = "Damage"
 * monsterDeathState.stateName = "Death"
 * monsterPatrolState.stateName = "Patrol"
 */