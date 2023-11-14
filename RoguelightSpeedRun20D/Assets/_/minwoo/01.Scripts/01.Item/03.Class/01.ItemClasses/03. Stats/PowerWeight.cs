using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerWeight : Stat, IProduct
{
    public PowerWeight(string name, int index) : base(name, index)
    {
        this.description = "You can attack harder.";
    }

    public override void Buy()
    {
        PlayerStatsManager.PowerWeight += 1;
        base.Buy();
    }
}
