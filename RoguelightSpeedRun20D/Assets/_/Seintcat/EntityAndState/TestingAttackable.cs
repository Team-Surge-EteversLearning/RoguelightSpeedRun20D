using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAttackable : AttackAble
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void _AttackStart()
    {

    }

    protected override void _AttackStop()
    {

    }

    protected override int _GetDamage(GameObject obj)
    {
        return damage;
    }
}