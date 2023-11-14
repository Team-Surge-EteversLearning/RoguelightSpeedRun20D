using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxStamina : Stat, IProduct
{
    public MaxStamina(string name, int index) : base(name, index)
    {
        this.description = "Increase your maxStamina";

    }
    public override void Buy()
    {
        PlayerStatsManager.StaminaMax += 1;
        base.Buy();
    }
}
