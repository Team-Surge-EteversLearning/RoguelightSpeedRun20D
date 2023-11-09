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
        _monsterBattleStates.Add(new MonsterBasic_Chase());
        _monsterBattleStates.Add(new MonsterBasic_Melee());

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
        if(target != null && target.Count > 0)
        {
            attackTarget = target[0];
            if (attackTarget != null && (mainState.stateName == monsterIdleState.stateName || mainState.stateName == monsterPatrolState.stateName))
            {
                ChangeState("Chase");
            }
            return;
        }
        attackTarget = null;
    }

    public override void Interrupt(string stateName)
    {

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
}
