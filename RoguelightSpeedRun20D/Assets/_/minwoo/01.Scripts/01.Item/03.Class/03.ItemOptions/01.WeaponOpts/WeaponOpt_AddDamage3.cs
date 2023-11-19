using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponOpt_AddDamage3 : EquipmentOption
{
    public WeaponOpt_AddDamage3(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
    {
        this.optName = "Enhance_3";
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Damage += 6;
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
