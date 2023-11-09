using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTestMob : MonsterSM
{
    private List<State> _monsterBattleStates = new List<State>();
    protected override List<State> monsterBattleStates => _monsterBattleStates;

    private void Awake()
    { 
        ManagerStart();
    }

    public override void MakeState()
    {
        // make custom states
        // make basic states
        _monsterBattleStates.Add(new MonsterBasic_Chase());
        _monsterBattleStates.Add(new MonsterBasic_Range());
        //_monsterBattleStates.Add(new MonsterBasic_Melee());
        base.MakeState();
    }

    void Start()
    {

    }
    void Update()
    {
        ManagerUpdate();
    }

    protected override void _FixedUpdate()
    {

    }

    public override void TargetChanged(List<GameObject> target)
    {
        if (target != null && target.Count > 0)
        {
            attackTarget = target[0];
            if (mainState.stateName == monsterIdleState.stateName || mainState.stateName == monsterPatrolState.stateName)
            {
                ChangeState("Chase");
            }
            return;
        }
        attackTarget = null;
    }

    public override void Interrupt(string stateName)
    {
        // not using
    }

    protected override void OnTriggerEnter(Collider other)
    {
        AttackAble otherAttack = other.GetComponent<AttackAble>();
        if (otherAttack != null && otherAttack.gameObject.tag == "PlayerAttack")
        {
            GetDamage(otherAttack.GetDamage(gameObject), other);
            if (mainBody.Raycast(new Ray(other.transform.position, other.transform.forward), out RaycastHit hit, 0.1f))
            {
                StateManager stateManager = hit.collider.gameObject.GetComponent<StateManager>();
                if (stateManager != null)
                    attackTarget = stateManager.gameObject;
            }
        }
    }

    protected override void ReactDamage()
    {

    }

    protected override void ResetStateMachine()
    {

    }
    private void OnFire()
    {
        attackAbles[0].gameObject.SetActive(true);
        attackAbles[0].gameObject.GetComponent<AttackAble>().AttackStart();
    }
}
