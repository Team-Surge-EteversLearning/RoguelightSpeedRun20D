﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponOpt_ReduceCoolTime2 : EquipmentOption
{
    public WeaponOpt_ReduceCoolTime2(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
    {
        this.optName = "Lightweightinge_2";
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Cooltime -= 0.06f;
        tempWeapon.usableOptions.Add(this);
        return tempWeapon;
    }

    public override GameObject MakeInGame(GameObject gameObject)
    {
        PlayerWeaponAttacks currentAttacks = gameObject.GetComponent<PlayerWeaponAttacks>(); //projectile
        WeaponOutfitHandle currentHandler = gameObject.GetComponent<WeaponOutfitHandle>(); // material

        return gameObject;
    }

    public override void UseOption()
    {
        return;
    }
}
