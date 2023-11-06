using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSM : StateManager, ITargetCatch
{
    [SerializeField]
    private Animator _animator;
    public Animator animator => _animator;

    [SerializeField]
    private MonsterData data;
    public MonsterData basicData => data;

    private int hpNow;

    protected abstract MonsterIdleState monsterIdleState { get; }
    protected abstract MonsterAttackState[] monsterAttackStates { get; }
    protected abstract MonsterProjectileState[] monsterProjectileStates { get; }
    protected MonsterDamageState monsterDamageState => new MonsterDamageState();
    protected MonsterDeathState monsterDeathState => new MonsterDeathState();

    public override void MakeState()
    {
        allStates = new Dictionary<string, State> { { monsterIdleState.stateName, monsterIdleState } };

        foreach (MonsterAttackState monsterAttackState in monsterAttackStates)
            allStates.Add(monsterAttackState.stateName, monsterAttackState);

        foreach (MonsterProjectileState monsterProjectileState in monsterProjectileStates)
            allStates.Add(monsterProjectileState.stateName, monsterProjectileState);

        allStates.Add(monsterDamageState.stateName, monsterDamageState);
        allStates.Add(monsterDeathState.stateName, monsterDeathState);

        mainState = allStates["Idle"];
        hpNow = data.hpMax;
    }

    public override void Interrupt(string stateName)
    {

    }

    protected abstract void OnTriggerEnter(Collider other);

    protected void GetDamage(int damage)
    {
        hpNow -= damage;

        if (hpNow < 0)
        {
            hpNow = 0; 
        }
        else
        {
            if (data.damagedStaggerTime > 0)
                ChangeState(monsterDamageState.stateName);

            ReactDamage(_animator);
        }
    }
    protected abstract void ReactDamage(Animator animator);

    public void _ResetStateMachine()
    {
        mainState.StateEnd_();
        ResetStateMachine();
        hpNow = data.hpMax;
        mainState = allStates["Idle"];
        enabled = false;
    }
    protected abstract void ResetStateMachine();

    public abstract void TargetChanged(List<GameObject> target);
}
