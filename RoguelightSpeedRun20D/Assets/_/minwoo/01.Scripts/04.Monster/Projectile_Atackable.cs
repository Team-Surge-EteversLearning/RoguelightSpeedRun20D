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
        rb = GetComponent<Rigidbody>();
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
        transform.position = lunchPoint.position;
        transform.LookAt(stateManager.attackTarget.transform.position);
        rb.velocity = Vector3.zero;
    }

    protected override void _AttackStart()
    {
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
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
}

