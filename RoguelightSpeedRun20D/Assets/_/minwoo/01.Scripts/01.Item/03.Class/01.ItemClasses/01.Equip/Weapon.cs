

using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    int damage;
    bool isRangeAttack;
    float size;
    float cooltime;

    WeaponData weaponData;

    public Weapon(string name, BasicEquipments basicData, WeaponData data, List<EquipmentOption> usableOptions, int tier = 0)
    {
        this.Name = name;
        this.SellWhenClear = basicData.SellWhenClear;
        this.Type = basicData.Type;
        this.damage = data.Damage;
        this.isRangeAttack = data.IsRangeAttack;
        this.size = data.Size;
        this.cooltime = data.Cooltime;
        this.ModelIndex = basicData.ModelIndex;
        this.usableOptions = new List<EquipmentOption> { };
        this.BasicData = basicData;
        this.weaponData = data;
        this.Tier = tier;

        Weapon thisWeapon = this;
        foreach (EquipmentOption option in usableOptions)
            thisWeapon = (Weapon)option.MakeEquipment(thisWeapon);
    }

    public int Damage { get => damage; set => damage = value; }
    public bool IsRangeAttack { get => isRangeAttack; set => isRangeAttack = value; }
    public float Size { get => size; set => size = value; }
    public float Cooltime { get => cooltime; set => cooltime = value; }
    public WeaponData WeaponData { get => weaponData; set => weaponData = value; }

    public override void Equip()
    {
        PlayerSM.weaponNow = this;
    }
}