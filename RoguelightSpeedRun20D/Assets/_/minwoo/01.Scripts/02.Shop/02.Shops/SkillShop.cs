using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SkillShop : Shop
{
    public override void InitShop(GameObject ui, Village village)
    {
        base.InitShop(ui, village);
        factory = new SkillDataModel();
    }
    public override void ResetShop()
    {
        Products.Clear();
        productSlots.Clear();

        Products = factory.Make();

        SettingShopUI();
        foreach (var item in BtnUIPair)
        {
            item.Key.GetComponentInChildren<TMP_Text>().text = "";
            item.Value.ThisShop = this;
        }
    }
}
