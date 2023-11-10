using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile_Atackable : AttackAble
{
    [SerializeField]
    private MonsterSM stateManager;
    [SerializeField]
    private int maxHitCount;
    [SerializeField]
    private float projectileSpeed;
    private Rigidbody rb;

    [SerializeField]
    Transform lunchPoint;
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = lunchPoint.position;
        if (stateManager.attackTarget != null)
        {
            transform.LookAt(stateManager.attackTarget.transform.position);
        }
        rb.velocity = Vector3.zero;

    }

    protected override void _AttackStart()
    {
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
    }

    protected override void _AttackStop()
    {

    }

    protected override int _GetDamage(GameObject obj)
    {
        if (obj.tag == "PlayerBody" && (!attackedObject.ContainsKey(obj) || attackedObject[obj] < maxHitCount))
        {
            gameObject.SetActive(false);
            Debug.Log(stateManager.basicData.attackPower);
            return stateManager.basicData.attackPower;
        }
        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        RangeMonsterSM manager = (RangeMonsterSM)stateManager;
        manager.projectiles.Add(gameObject);
    }
}

