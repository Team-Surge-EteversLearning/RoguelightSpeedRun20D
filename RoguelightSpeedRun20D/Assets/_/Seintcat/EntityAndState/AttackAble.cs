using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAble : MonoBehaviour
{
    [SerializeField]
    private Collider attackTrigger;

    private DateTime _lastAttackTime;
    public DateTime lastAttackTime
    {
        get
        {
            return lastAttackTime;
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackStart()
    {
    }
}
