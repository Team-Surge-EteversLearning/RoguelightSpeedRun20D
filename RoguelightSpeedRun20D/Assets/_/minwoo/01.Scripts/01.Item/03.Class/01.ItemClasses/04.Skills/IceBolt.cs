using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IceBolt : ActiveSkill
{
    public IceBolt(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
        SkillDecription += " : Elsa? Let it go~";
    }

    protected override void Init()
    {
        base.Init();
    }
        
    public override void _Use()
    {
        Debug.LogWarning(Name);
        PlayerSM playerSM = PlayerSM.playerObj.GetComponent<PlayerSM>();
        GameObject projectile = playerSM.skillPrefabs[1];
        //SetRotation
        Vector3 direction = playerSM.skillTransform.forward;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject instantiatedObject = UnityEngine.Object.Instantiate(projectile, playerSM.skillTransform.position, rotation);
        instantiatedObject.GetComponent<AttackAble>().AttackStart();
    }
}

