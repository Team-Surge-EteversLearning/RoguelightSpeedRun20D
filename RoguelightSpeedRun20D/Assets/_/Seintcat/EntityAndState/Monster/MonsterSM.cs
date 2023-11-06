using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSM : StateManager
{
    [SerializeField]
    private Animator _animator;
    public Animator animator => _animator;

    [SerializeField]
    private MonsterData data;
    public MonsterData basicData => data;

    public bool foundPlayer { get; private set; }
    public bool damaged { get; private set; }

    private int hpNow;

    protected abstract MonsterIdleState monsterIdleState { get; }
    protected abstract MonsterAttackState[] monsterAttackStates { get; }
    protected abstract MonsterProjectileState[] monsterProjectileStates { get; }
    protected MonsterDamageState monsterDamageState => new MonsterDamageState();
    protected abstract MonsterDeathState MonsterDeathState { get; }

    public override void MakeState()
    {
        allStates = new Dictionary<string, State> { { monsterIdleState.stateName, monsterIdleState } };

        foreach (MonsterAttackState monsterAttackState in monsterAttackStates)
            allStates.Add(monsterAttackState.stateName, monsterAttackState);

        foreach (MonsterProjectileState monsterProjectileState in monsterProjectileStates)
            allStates.Add(monsterProjectileState.stateName, monsterProjectileState);

        if (monsterDamageState != null)
            allStates.Add(monsterDamageState.stateName, monsterDamageState);

        if (MonsterDeathState != null)
            allStates.Add(MonsterDeathState.stateName, MonsterDeathState);

        mainState = allStates["Idle"];
    }

    public override void Interrupt(string stateName)
    {
        switch(stateName)
        {
            case "EyeEnter":
                foundPlayer = true;
                break;
            case "EyeExit":
                foundPlayer = false;
                break;
            default:
                break;
        }
    }

    protected abstract void OnTriggerEnter(Collider other);

    protected void GetDamage(int damage)
    {
        hpNow -= damage;
    }
}
