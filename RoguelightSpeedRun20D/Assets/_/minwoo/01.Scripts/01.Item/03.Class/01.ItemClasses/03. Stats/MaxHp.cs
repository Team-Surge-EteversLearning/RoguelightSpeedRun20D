using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHp : Stat, IProduct
{
    public MaxHp(string name, int index) : base(name, index)
    {
    }

    public override void Buy()
    {
        PlayerStatsManager.HpMax += 1;
        base.Buy();
    }
}
