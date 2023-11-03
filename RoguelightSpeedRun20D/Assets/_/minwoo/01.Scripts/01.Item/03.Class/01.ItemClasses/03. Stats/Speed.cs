using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Stat, IProduct
{
    public Speed(string name, int index) : base(name, index)
    {
    }
    public override void Buy()
    {
        PlayerStatsManager.Speed += 1;
        base.Buy();
    }
}
