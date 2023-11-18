using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponOpt_AngelPower: EquipmentOption
{
    public override Equipment MakeEquipment(Equipment equipment)
    {
        this.optName = "AngelPower";
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Damage += 5;
        tempWeapon.Cooltime -= 0.01f;
        tempWeapon.usableOptions.Add(this);
        return tempWeapon;
    }

    public override GameObject MakeInGame(GameObject gameObject)
    {
        PlayerWeaponAttacks currentAttacks = gameObject.GetComponent<PlayerWeaponAttacks>(); //projectile
        WeaponOutfitHandle currentHandler = gameObject.GetComponent<WeaponOutfitHandle>(); // material
        currentHandler.indexMaterial = 2;

        return gameObject;
    }

    public override void UseOption()
    {
        PlayerSM.hpNow += 5;
    }
}
