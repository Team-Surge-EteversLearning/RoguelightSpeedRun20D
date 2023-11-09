using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamagedPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            AttackAble otherAttack = other.GetComponent<AttackAble>();
            otherAttack.GetDamage(gameObject);
        }
    }
}
