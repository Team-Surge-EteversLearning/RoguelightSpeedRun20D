using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class EquipShop : Shop
{
    private string range;

    public override void InitShop(GameObject ui, Village village)
    {
        base.InitShop(ui, village);
        factory = new EquipmentDataManager();
    }
    public EquipShop(int minTier, int maxTier)
    {
        this.range = $"{minTier},{maxTier},";
    }

    public override void ResetShop()
    {  
        products.Clear();
        productSlots.Clear();

        products = factory.Make($"{range}2");
        SettingShopUI();
        OpenShop();
    }
}
