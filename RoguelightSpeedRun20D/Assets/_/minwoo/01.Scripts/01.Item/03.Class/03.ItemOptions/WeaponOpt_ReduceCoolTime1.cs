using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponOpt_ReduceCoolTime1 : EquipmentOption
{
    public override Equipment MakeEquipment(Equipment equipment)
    {
        this.optName = "Lightweighting_UnCommon";
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Cooltime -= 0.4f;
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
