using System.Collections.Generic;

public class Shoes : Equipment
{
    int maxHp;
    int speed;
    int maxStamina;
    float staminaRegen;

    public Shoes(string name, BasicEquipments basicData, ShoesData data, List<EquipmentOption> usableOptions, int tier = 0)
    {
        this.Name = name;
        this.SellWhenClear = basicData.SellWhenClear;
        this.Type = basicData.Type;
        this.MaxHp = data.MaxHp;
        this.Speed = data.Speed;
        this.MaxStamina = data.MaxStamina;
        this.StaminaRegen = data.StaminaRegen;
        this.ModelIndex = basicData.ModelIndex;
        this.usableOptions = new List<EquipmentOption> { };
        this.Tier = tier;

        Shoes thisShoes = this;
        foreach (EquipmentOption option in usableOptions)
            thisShoes = (Shoes)option.MakeEquipment(thisShoes);

    }

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Speed { get => speed; set => speed = value; }
    public int MaxStamina { get => maxStamina; set => maxStamina = value; }
    public float StaminaRegen { get => staminaRegen; set => staminaRegen = value; }

    public override void Equip()
    {
        PlayerSM.shoesNow = this;
    }
}