﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponOpt_AddDamage0 : EquipmentOption
{
    public WeaponOpt_AddDamage0(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
    {
        this.optName = "Enhance_0";
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Damage += 3;
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
