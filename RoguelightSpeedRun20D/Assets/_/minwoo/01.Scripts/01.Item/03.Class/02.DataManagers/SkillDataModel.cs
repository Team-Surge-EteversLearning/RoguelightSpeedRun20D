using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SkillDataModel : IProductMaker
{
    static Dictionary<string, ActiveSkill> unlockActive;
    static Dictionary<string, ActiveSkill> lockActive;

    static int skillBookPrice;
    void Init()
    {
        //create all skills
        //lockActive add all skills
        //unlockSkills remove at lockaActive and add to unlockActive
    }

    public List<ShopProduct> Make(string info)
    {
        List<ShopProduct> displayItemListWithPrice = new List<ShopProduct>();
        Random rand = new Random();
        List<ActiveSkill> skillList = lockActive.Values.ToList();
        int index = rand.Next(skillList.Count); // 0 ~ (dictList.Count - 1) 범위의 랜덤 정수 생성

        ActiveSkill randomSkill = skillList[index];

        ShopProduct product = new ShopProduct(randomSkill, skillBookPrice);
        displayItemListWithPrice.Add(product);

        return displayItemListWithPrice;
    }
}

