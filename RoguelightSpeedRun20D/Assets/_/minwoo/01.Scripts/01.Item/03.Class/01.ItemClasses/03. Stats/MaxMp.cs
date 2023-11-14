using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMp : Stat, IProduct
{
    public MaxMp(string name, int index) : base(name, index)
    {
        this.description = "Increase your maxMana";

    }
    public override void Buy()
    {
        PlayerStatsManager.ManaMax += 1;
        base.Buy();

    }
}
