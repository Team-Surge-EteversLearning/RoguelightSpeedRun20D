using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAble : MonoBehaviour
{
    [SerializeField]
    protected Collider attackTrigger;
    [SerializeField]
    protected float attackTime = 1f;

    protected Dictionary<GameObject, int> attackedObject = new Dictionary<GameObject, int>();

    protected abstract void _AttackStart();
    public void AttackStart()
    {
        attackTrigger.enabled = true;
        _AttackStart();
        Invoke("AttackStop", attackTime);
    }
    
    protected abstract void _AttackStop();
    public void AttackStop()
    {
        _AttackStop();
        attackTrigger.enabled = false;
        attackedObject.Clear();
    }

    private void Touch(GameObject obj)
    {
        if(attackedObject.ContainsKey(obj))
            attackedObject[obj]++;
        else
            attackedObject.Add(obj, 0);
    }

    protected abstract int _GetDamage(GameObject obj);
    public int GetDamage(GameObject obj)
    {
        int val = _GetDamage(obj);
        Touch(obj);
        return val;
    }
}
