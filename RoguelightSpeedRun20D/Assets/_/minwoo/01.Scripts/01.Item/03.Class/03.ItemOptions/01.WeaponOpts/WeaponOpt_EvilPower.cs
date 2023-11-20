using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponOpt_EvilPower: EquipmentOption
{
    public WeaponOpt_EvilPower(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
    {
        this.optName = "EvilPower";
        Weapon tempWeapon = (Weapon)equipment;
        tempWeapon.Damage += 10;
        tempWeapon.Cooltime += 0.1f;
        tempWeapon.usableOptions.Add(this);
        return tempWeapon;
    }

    public override GameObject MakeInGame(GameObject gameObject)
    {
        PlayerWeaponAttacks currentAttacks = gameObject.GetComponent<PlayerWeaponAttacks>(); //projectile
        WeaponOutfitHandle currentHandler = gameObject.GetComponent<WeaponOutfitHandle>(); // material
        currentHandler.indexMaterial = 1;

        return gameObject;
    }

    public override void UseOption()
    {
        PlayerSM.hpNow += 5;
    }
}
