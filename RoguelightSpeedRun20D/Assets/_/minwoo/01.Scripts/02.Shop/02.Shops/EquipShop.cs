using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipShop : Shop
{
    private string range;

    public override void InitShop(GameObject ui, Village village)
    {
        base.InitShop(ui, village);
        factory = new EquipmentDataManager();

        Products.Clear();
        Products = factory.Make(range);
    }
    public EquipShop(int minTier, int maxTier, int amount)
    {
        this.range = $"{minTier},{maxTier},{amount},V";
    }

    public override void ResetShop()
    {
        productSlots.Clear();

        SettingShopUI();

        foreach ( var item in BtnUIPair ) 
        {
            item.Key.GetComponentInChildren<TMP_Text>().text = "";
            item.Value.ThisShop = this;
        }
    }

}
