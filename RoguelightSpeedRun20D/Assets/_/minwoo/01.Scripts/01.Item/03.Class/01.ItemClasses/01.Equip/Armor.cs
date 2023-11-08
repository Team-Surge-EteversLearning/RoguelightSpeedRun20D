public class Armor : Equipment
{
    int maxHp;
    bool trapAvoid;
    int maxMana;
    float manaRegen;
    ArmorData _data { get; set; }

    public Armor(string name, BasicEquipments basicData, ArmorData data)
    {
        this.Name = name;
        this.SellWhenClear = basicData.SellWhenClear;
        this.Type = basicData.Type;
        this.MaxHp = data.MaxHp;
        this.TrapAvoid = data.TrapAvoid;
        this.MaxMana = data.MaxMana;
        this.ManaRegen = data.ManaRegen;
        this.ModelIndex = basicData.ModelIndex;
    }

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public bool TrapAvoid { get => trapAvoid; set => trapAvoid = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    public float ManaRegen { get => manaRegen; set => manaRegen = value; }

    public override void Equip()
    {
        PlayerSM.armorNow = this;
    }
}