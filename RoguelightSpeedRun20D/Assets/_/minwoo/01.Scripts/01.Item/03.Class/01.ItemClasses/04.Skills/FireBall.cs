using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FireBall : ActiveSkill
{
    public FireBall(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
    }

    protected override void Init()
    {
        base.Init();
    }
        
    public override void Use()
    {
        PlayerSM playerSM = PlayerSM.playerObj.GetComponent<PlayerSM>();
        GameObject projectile = playerSM.skillPrefabs[0];
        //SetRotation
        Vector3 direction = playerSM.skillTransform.forward; 
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject instantiatedObject = UnityEngine.Object.Instantiate(projectile, playerSM.skillTransform.position, rotation);
        instantiatedObject.GetComponent<AttackAble>().AttackStart();
    }
}

