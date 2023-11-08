
public struct WeaponData
{
    int damage;
    bool isRangeAttack;
    float size;
    float cooltime;

    public WeaponData(int damage, bool isRangeAttack, int size, float cooltime)
    {
        this.damage = damage;
        this.isRangeAttack = isRangeAttack;
        this.size = size;
        this.cooltime = cooltime;
    }

    public int Damage { get => damage; set => damage = value; }
    public bool IsRangeAttack { get => isRangeAttack; set => isRangeAttack = value; }
    public float Size { get => size; set => size = value; }
    public float Cooltime { get => cooltime; set => cooltime = value; }
}
