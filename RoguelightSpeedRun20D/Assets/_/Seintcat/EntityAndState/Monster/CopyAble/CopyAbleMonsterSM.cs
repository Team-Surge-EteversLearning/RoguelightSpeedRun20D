using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Don't edit this, Copy this!
public class CopyAbleMonsterSM : MonsterSM
{
    private MonsterIdleState _monsterIdleState;
    private MonsterAttackState[] _monsterAttackStates;
    private MonsterProjectileState[] _monsterProjectileStates;
    private MonsterDeathState _MonsterDeathState;

    protected override MonsterIdleState monsterIdleState => throw new System.NotImplementedException();
    protected override MonsterAttackState[] monsterAttackStates => throw new System.NotImplementedException();
    protected override MonsterProjectileState[] monsterProjectileStates => throw new System.NotImplementedException();

    private void Awake()
    {
        ManagerStart();
    }

    public override void MakeState()
    {
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
        ManagerUpdate();
    }

    public override void TargetChanged(List<GameObject> target)
    {
        throw new System.NotImplementedException();
    }

    public override void Interrupt(string stateName)
    {

    }

    protected override void OnTriggerEnter(Collider other)
    {
        AttackAble otherAttack = other.GetComponent<AttackAble>();
        if (otherAttack != null)
        {
            GetDamage(otherAttack.GetDamage(gameObject));
        }
    }

    protected override void ReactDamage(Animator animator)
    {
        throw new System.NotImplementedException();
    }

    protected override void ResetStateMachine()
    {
        throw new System.NotImplementedException();
    }
}
