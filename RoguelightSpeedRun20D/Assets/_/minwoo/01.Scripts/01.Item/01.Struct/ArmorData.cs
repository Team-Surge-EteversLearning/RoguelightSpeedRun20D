
public struct ArmorData
{
    int maxHp;
    bool trapAvoid;
    int maxMana;
    float manaRegen;

    public ArmorData(int maxHp, bool trapAvoid, int maxMana, float manaRegen)
    {
        this.maxHp = maxHp;
        this.trapAvoid = trapAvoid;
        this.maxMana = maxMana;
        this.manaRegen = manaRegen;
    }

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public bool TrapAvoid { get => trapAvoid; set => trapAvoid = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    public float ManaRegen { get => manaRegen; set => manaRegen = value; }
}
