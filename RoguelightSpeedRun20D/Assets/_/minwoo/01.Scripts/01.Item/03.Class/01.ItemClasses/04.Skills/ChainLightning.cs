using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : ActiveSkill
{
    public ChainLightning(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
        SkillDecription += " : pakin!";
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void _Use()
    {
        PlayerSM playerSM = PlayerSM.playerObj.GetComponent<PlayerSM>();
        GameObject projectile = playerSM.skillPrefabs[3];
        //SetRotation
        Vector3 direction = playerSM.skillTransform.forward;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject instantiatedObject = UnityEngine.Object.Instantiate(projectile, playerSM.skillTransform.position, rotation);
        instantiatedObject.GetComponent<AttackAble>().AttackStart();


    }
}
