using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Don't edit this, Copy this!
public class GhoulSM : MonsterSM
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
        _monsterBattleStates.Add(new Basic_Chase());

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
        if (!mainState.started)
        {
            if (mainState.stateName == monsterPatrolState.stateName)
            {
                subBody[0].enabled = false;
                subBody[1].enabled = true;
            }
            if (mainState.stateName == monsterIdleState.stateName)
            {
                subBody[0].enabled = true;
                subBody[1].enabled = false;
            }
        }
        ManagerUpdate();
    }

    protected override void _FixedUpdate()
    {

    }

    public override void TargetChanged(List<GameObject> target)
    {
        if (target != null && 
           (mainState.stateName == monsterIdleState.stateName || mainState.stateName == monsterPatrolState.stateName))
        {
            attackTarget = target[0];
            ChangeState("Chase");
        }
        else
        {
            attackTarget = null;
        }
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

    }

    protected override void ResetStateMachine()
    {

    }
}
