﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponOpt_AddDamage0 : EquipmentOption
{
    public override Equipment MakeEquipment(Equipment equipment)
    {
        this.optName = "Enhance_Common";
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Damage += 5;
        tempWeapon.usableOptions.Add(this);
        return tempWeapon;
    }

    public override GameObject MakeInGame(GameObject gameObject)
    {
        PlayerWeaponAttacks currentAttacks = gameObject.GetComponent<PlayerWeaponAttacks>(); //projectile
        WeaponOutfitHandle currentHandler = gameObject.GetComponent<WeaponOutfitHandle>(); // material
        currentHandler.indexMaterial = 0;

        return gameObject;
    }

    public override void UseOption()
    {
        return;
    }
}
