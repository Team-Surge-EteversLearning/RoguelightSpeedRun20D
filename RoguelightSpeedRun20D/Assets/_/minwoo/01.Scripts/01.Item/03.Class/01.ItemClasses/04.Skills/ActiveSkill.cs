
using UnityEngine;

public abstract class ActiveSkill : IProduct
{
    public static GameObject projectile;

    protected string name;
    protected int mana;
    protected float coolTime;

    protected ActiveSkill(string name, int mana, float coolTime)
    {
        this.name = name;
        this.mana = mana;
        this.coolTime = coolTime;
        Init();
    }

    public void Buy()
    {
        if (!SkillDataModel.UnlockActive.ContainsKey(name))
            SkillDataModel.UnlockActive.Add(name, this);
        Debug.Log(this.name);
    }

    protected virtual void Init()
    {
        SkillDataModel.LockActive.Add(name, this);
    }
    public abstract void Use(int mana, out float coolTime);
}