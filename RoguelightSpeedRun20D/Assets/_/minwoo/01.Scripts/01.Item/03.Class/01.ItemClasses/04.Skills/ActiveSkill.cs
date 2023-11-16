
using UnityEngine;

public abstract class ActiveSkill : IProduct
{
    private string name;
    private int mana;
    private float coolTime;
    private string _skillDecription;


    public string Name { get => name; set => name = value; }
    public int Mana { get => mana; set => mana = value; }
    public float CoolTime { get => coolTime; set => coolTime = value; }

    string IProduct.key { get => throw new System.NotImplementedException(); }
    public string SkillDecription { get => _skillDecription; set => _skillDecription = value; }

    protected ActiveSkill(string name, int mana, float coolTime)
    {
        this.Name = name;
        this.Mana = mana;
        this.CoolTime = coolTime;
        SkillDecription = name;
        Init();
    }

    public void Buy()
    {
        if (!SkillDataModel.UnlockActive.ContainsKey(Name))
            SkillDataModel.UnlockActive.Add(Name, this);
        SkillPanelManager.onLearnSkill?.Invoke(this);
        Debug.Log(this.Name);
    }

    protected virtual void Init()
    {
        SkillDataModel.LockActive.Add(Name, this);
    }
    public void Equip()
    {

    }
    public abstract void _Use();
    public void Use(bool skill1)
    {
        if (PlayerSM.manaNow < mana)
        {
            return;
        }

        if (skill1)
        {
            PlayerSM.skill1CoolTime = coolTime;
        }
        else
        {
            PlayerSM.skill2CoolTime = coolTime;
        }

        PlayerSM.manaNow -= mana;
        _Use();
    }
}