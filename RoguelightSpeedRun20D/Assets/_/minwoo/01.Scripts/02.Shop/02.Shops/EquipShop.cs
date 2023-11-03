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

    public EquipShop(int minTier, int maxTier)
    {
        this.range = $"{minTier},{maxTier},";
    }

    public override void ResetShop()
    {  
        products.Clear();
        productSlots.Clear();

        EquipmentDataManager equipFactory = new EquipmentDataManager();
        products = equipFactory.Make($"{range}2");
        SettingShopUI();
        OpenShop();
    }
}
