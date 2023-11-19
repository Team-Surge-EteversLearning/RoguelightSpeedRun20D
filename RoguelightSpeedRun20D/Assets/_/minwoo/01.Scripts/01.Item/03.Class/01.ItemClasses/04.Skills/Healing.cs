using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : ActiveSkill
{
    public Healing(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
        SkillDecription += " : Mom's touch";
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void _Use()
    {
        PlayerSM playerSM = PlayerSM.playerObj.GetComponent<PlayerSM>();
        GameObject aura = playerSM.skillPrefabs[2];
        GameObject instantiatedObject = UnityEngine.Object.Instantiate(aura, playerSM.skillTransform.position, Quaternion.identity);
        instantiatedObject.transform.parent = playerSM.transform;
        PlayerSM.hpNow = PlayerSM.hpNow + (int)(PlayerSM.hpMax * 0.2f);
    }
}
