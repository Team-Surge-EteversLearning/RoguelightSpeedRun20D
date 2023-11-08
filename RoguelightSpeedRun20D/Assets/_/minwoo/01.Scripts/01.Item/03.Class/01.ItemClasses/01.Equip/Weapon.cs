

using UnityEngine;

public class Weapon : Equipment
{
    int damage;
    bool isRangeAttack;
    int size;
    float cooltime;

    public Weapon(string name, BasicEquipments basicData, WeaponData data)
    {
        this.Name = name;
        this.SellWhenClear = basicData.SellWhenClear;
        this.Type = basicData.Type;
        this.damage = data.Damage;
        this.isRangeAttack = data.IsRangeAttack;
        this.size = data.Size;
        this.cooltime = data.Cooltime;
        this.ModelIndex = basicData.ModelIndex;

    }

    public int Damage { get => damage; set => damage = value; }
    public bool IsRangeAttack { get => isRangeAttack; set => isRangeAttack = value; }
    public int Size { get => size; set => size = value; }
    public float Cooltime { get => cooltime; set => cooltime = value; }

    public override void Equip()
    {
        PlayerSM.weaponNow = this;
    }
}