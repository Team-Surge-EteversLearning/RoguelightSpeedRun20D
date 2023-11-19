using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
    int maxHp;
    bool trapAvoid;
    int maxMana;
    float manaRegen;
    ArmorData _data { get; set; }

    public Armor(string name, BasicEquipments basicData, ArmorData data, List<EquipmentOption> usableOptions,int sellWhenClear, int tier = 0)
    {
        this.Name = name;
        this.SellWhenClear = basicData.SellWhenClear;
        this.Type = basicData.Type;
        this.MaxHp = data.MaxHp;
        this.TrapAvoid = data.TrapAvoid;
        this.MaxMana = data.MaxMana;
        this.ManaRegen = data.ManaRegen;
        this.ModelIndex = basicData.ModelIndex;
        this.usableOptions = new List<EquipmentOption> { };
        this.Tier = tier;
        this.SellWhenClear = sellWhenClear;

        Armor thisArmor = this;
        foreach (EquipmentOption option in usableOptions)
            thisArmor = (Armor)option.MakeEquipment(thisArmor);
    }

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public bool TrapAvoid { get => trapAvoid; set => trapAvoid = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    public float ManaRegen { get => manaRegen; set => manaRegen = value; }

    public override void Equip()
    {

        PlayerOutfitSelecter.Instance.armorNow = this;
        PlayerSM.armorNow = this;
    }
}