public class Armor : Equipment
{
    int maxHp;
    bool trapAvoid;
    int maxMana;
    float manaRegen;

    public Armor(string name, BasicEquipments basicData, ArmorData data)
    {
        this.name = name;
        this.sellWhenClear = basicData.SellWhenClear;
        this.type = basicData.Type;
        this.MaxHp = data.MaxHp;
        this.TrapAvoid = data.TrapAvoid;
        this.MaxMana = data.MaxMana;
        this.ManaRegen = data.ManaRegen;
    }

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public bool TrapAvoid { get => trapAvoid; set => trapAvoid = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    public float ManaRegen { get => manaRegen; set => manaRegen = value; }
}