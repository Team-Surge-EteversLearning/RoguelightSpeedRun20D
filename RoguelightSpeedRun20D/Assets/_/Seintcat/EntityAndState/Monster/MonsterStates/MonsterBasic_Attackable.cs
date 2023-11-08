using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasic_Attackable : AttackAble
{
    [SerializeField]
    private MonsterSM stateManager;
    [SerializeField]
    private int maxHitCount;

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

    protected override void _AttackStart()
    {

    }

    protected override void _AttackStop()
    {

    }

    protected override int _GetDamage(GameObject obj)
    {
        if(obj.tag == "PlayerBody" && (!attackedObject.ContainsKey(obj) || attackedObject[obj] < maxHitCount))
            return stateManager.basicData.attackPower;

        return 0;
    }
}
