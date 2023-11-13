using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponOpt_AddDamage : EquipmentOption
{
    public override Equipment MakeEquipment(Equipment equipment)
    {
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Damage += 10;
        tempWeapon.usableOptions.Add(this);
        return tempWeapon;
    }

    public override GameObject MakeInGame(GameObject gameObject)
    {
        PlayerWeaponAttacks currentAttacks = gameObject.GetComponent<PlayerWeaponAttacks>();
        WeaponOutfitHandle currentHandler = gameObject.GetComponent<WeaponOutfitHandle>();
        currentHandler.indexMaterial = 2;

        return gameObject;
    }

    public override void UseOption()
    {
        return;
    }
}
