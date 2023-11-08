public class Shoes : Equipment
{
    int maxHp;
    float speed;
    int maxStamina;
    float staminaRegen;

    public Shoes(string name, BasicEquipments basicData, ShoesData data)
    {
        this.Name = name;
        this.SellWhenClear = basicData.SellWhenClear;
        this.Type = basicData.Type;
        this.MaxHp = data.MaxHp;
        this.Speed = data.Speed;
        this.MaxStamina = data.MaxStamina;
        this.StaminaRegen = data.StaminaRegen;
        this.ModelIndex = basicData.ModelIndex;

    }

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public float Speed { get => speed; set => speed = value; }
    public int MaxStamina { get => maxStamina; set => maxStamina = value; }
    public float StaminaRegen { get => staminaRegen; set => staminaRegen = value; }

    public override void Equip()
    {
        PlayerSM.shoesNow = this;
    }
}