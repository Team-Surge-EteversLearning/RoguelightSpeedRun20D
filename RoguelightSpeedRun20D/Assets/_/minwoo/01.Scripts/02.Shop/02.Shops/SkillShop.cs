using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SkillShop : Shop
{
    public override void ResetShop()
    {
        products.Clear();
        productSlots.Clear();

        SkillDataModel skillFactory = new SkillDataModel();
        products = skillFactory.Make();

        SettingShopUI();
        OpenShop();
    }
}
