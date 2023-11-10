using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MonsterSM : StateManager, ITargetCatch
{
    [SerializeField]
    private Animator _animator;
    public Animator animator => _animator;

    [SerializeField]
    private MonsterData data;
    public MonsterData basicData => data;

    [SerializeField]
    protected Rigidbody _rigidbody;
    [SerializeField]
    protected GameObject eye;

    public Vector3 targetPos
    {
        get
        {
            if (patrolMoved)
            {
                patrolMoved = false;

                if (!patrolForward)
                    patrolIndex--;
                else
                    patrolIndex++;

                switch (data.patrolMode)
                {
                    case PatrolMode.Line:
                        if (patrolIndex < 0)
                        {
                            patrolIndex = 1;
                            patrolForward = true;
                        }
                        else if (patrolIndex >= movePoints.Count)
                        {
                            patrolIndex = movePoints.Count - 2;
                            patrolForward = false;
                        }
                        break;

                    case PatrolMode.Circle:
                        if (patrolIndex >= movePoints.Count)
                            patrolIndex = 0;
                        break;

                    case PatrolMode.Random:
                        patrolIndex = Random.Range(0, movePoints.Count);
                        break;

                    default:
                        break;
                }
            }
            return movePoints[patrolIndex].position;
        }
    }

    public Collider mainBody;
    public List<Collider> subBody;
    public List<AttackAble> attackAbles;
    public List<Transform> movePoints;

    private int hpNow;
    private int patrolIndex = 0;
    private bool patrolForward = true;
    private bool patrolMoved = false;

    protected State monsterIdleState = new MonsterIdleState();
    protected State monsterDamageState = new MonsterDamageState();
    protected State monsterDeathState = new MonsterDeathState();
    protected State monsterPatrolState = new MonsterPatrolState();
    protected abstract List<State> monsterBattleStates { get; }

    public GameObject attackTarget { get; protected set; }

    public override void MakeState()
    {
        allStates = new Dictionary<string, State> 
        { 
            { monsterIdleState.stateName, monsterIdleState },
            { monsterDamageState.stateName, monsterDamageState },
            { monsterDeathState.stateName, monsterDeathState },
            { monsterPatrolState.stateName, monsterPatrolState },
        };

        if(monsterBattleStates != null)
            foreach (State monsterBattleState in monsterBattleStates)
                allStates.Add(monsterBattleState.stateName, monsterBattleState);

        mainState = allStates[monsterIdleState.stateName];
        hpNow = data.hpMax;

        if (eye != null)
            eye.SetActive(true);
    }

    private void Update()
    {
        ManagerUpdate();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.isKinematic)
            return;

        _rigidbody.velocity *= (1 - Time.fixedDeltaTime);

        if (mainState.stateName == monsterPatrolState.stateName ||
            mainState.stateName == "Chase")
        {
            _rigidbody.AddForce(transform.forward * Time.fixedDeltaTime * (mainState.stateName == "Chase" ? data.chaseSpeed : data.moveSpeed), ForceMode.VelocityChange);
            _FixedUpdate();
        }
    }
    protected abstract void _FixedUpdate();

    public override void Interrupt(string stateName)
    {

    }

    protected abstract void OnTriggerEnter(Collider other);

    protected void GetDamage(int damage, Collider other)
    {
        //Debug.LogWarning(damage);
        hpNow -= damage;

        foreach (AttackAble attackAble in attackAbles)
            attackAble.AttackStop();

        if (hpNow < 0)
        {
            foreach (Collider collider in subBody)
                collider.enabled = false;

            hpNow = 0;
            
            _rigidbody.isKinematic = true;
            mainBody.enabled = false;
            ChangeState(monsterDeathState.stateName);
        }
        else
        {
            foreach (Collider collider in subBody)
                collider.enabled = false;

            if (data.damagedStaggerTime > 0)
                ChangeState(monsterDamageState.stateName);

            ReactDamage();
        }
    }
    protected abstract void ReactDamage();

    public void _ResetStateMachine()
    {
        mainState.StateEnd_();
        ResetStateMachine();
        hpNow = data.hpMax;
        mainBody.enabled = true;
        _rigidbody.isKinematic = false;
        patrolIndex = 0;
        patrolForward = true;
        mainState = allStates[monsterIdleState.stateName]; 
        enabled = false;
    }
    protected abstract void ResetStateMachine();

    public abstract void TargetChanged(List<GameObject> target);

    public Transform NextPatrol()
    {
        patrolMoved = true;
        return movePoints[patrolIndex];
    }
}
