using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        products.Clear();
        productSlots.Clear();

        products = factory.Make();

        SettingShopUI();
        OpenShop();
    }
}
