
public struct ShoesData
{
    int maxHp;
    float speed;
    int maxStamina;
    float staminaRegen;

    public ShoesData(int maxHp, float speed, int maxStamina, float staminaRegen)
    {
        this.maxHp = maxHp;
        this.speed = speed;
        this.maxStamina = maxStamina;
        this.staminaRegen = staminaRegen;
    }

    public int MaxHp { get => maxHp; set => maxHp = value; }
    public float Speed { get => speed; set => speed = value; }
    public int MaxStamina { get => maxStamina; set => maxStamina = value; }
    public float StaminaRegen { get => staminaRegen; set => staminaRegen = value; }
}
