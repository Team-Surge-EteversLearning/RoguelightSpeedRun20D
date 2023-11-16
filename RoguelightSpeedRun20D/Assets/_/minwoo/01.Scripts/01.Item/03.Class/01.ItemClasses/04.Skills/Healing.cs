using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : ActiveSkill
{
    public Healing(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
        SkillDecription = "Mom's touch";
    }

    protected override void Init()
    {
        base.Init();
    }

    public override void _Use()
    {
        PlayerSM.hpNow = PlayerSM.hpNow + (int)(PlayerSM.hpMax * 0.2f);
    }
}
