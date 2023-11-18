using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkillDataModel : IProductMaker
{
    static Dictionary<string, ActiveSkill> unlockActive = new Dictionary<string, ActiveSkill>();
    static Dictionary<string, ActiveSkill> lockActive = new Dictionary<string, ActiveSkill>();

    static int skillBookPrice;

    public static Dictionary<string, ActiveSkill> LockActive { get => lockActive; set => lockActive = value; }
    public static Dictionary<string, ActiveSkill> UnlockActive { get => unlockActive; set => unlockActive = value; }

    public void Init()
    {
        //create all skills
        //lockActive add all skills
        //unlockSkills remove at lockaActive and add to unlockActive
        FireBall fireBall = new FireBall("fireBall", 1, 1);
        //IceBolt iceBolt = new IceBolt("iceBolt", 10, 1);
        //SwordShoot swordShoot = new SwordShoot("swordShoot", 10, 1);
        //BerSerk berSerk = new BerSerk("berSerk", 10, 1);
        //Healing healing = new Healing("healing", 10, 1);
    }

    public List<ShopProduct> Make(string info = "")
    {
        List<ShopProduct> displayItemListWithPrice = new List<ShopProduct>();
        System.Random rand = new System.Random();
        List<ActiveSkill> skillList = LockActive.Values.ToList();
        Debug.LogWarning(skillList.Count);
        int index = rand.Next(skillList.Count); // 0 ~ (dictList.Count - 1) 범위의 랜덤 정수 생성

        ActiveSkill randomSkill = skillList[index];

        ShopProduct product = new ShopProduct(randomSkill, skillBookPrice);
        displayItemListWithPrice.Add(product);

        return displayItemListWithPrice;
    }

    public static void Load(List<string> itemUnlock)
    {

    }
}

