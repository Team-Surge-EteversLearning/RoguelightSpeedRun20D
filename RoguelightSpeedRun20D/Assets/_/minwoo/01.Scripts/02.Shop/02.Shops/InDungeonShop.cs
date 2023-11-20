using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class InDungeonShop : Shop
{
    private string range;
    private string type;

    public override void InitShop(GameObject ui, DungeonShopManager dsManager)
    {
        base.InitShop(ui, dsManager);
        factory = new EquipmentDataManager();

        Products.Clear();
        Products = factory.Make(range);

        factory = new DungeonItemManager();
        products.AddRange(factory.Make(type));
    }
    public InDungeonShop(int minTier, int maxTier, int amount, string type)
    {
        this.range = $"{minTier},{maxTier},{amount},D";
        this.type = type;
    }

    public override void ResetShop()
    {
        productSlots.Clear();

        SettingShopUI();
        OpenShop();

        foreach (var item in BtnUIPair)
        {
            item.Key.GetComponentInChildren<TMP_Text>().text = "";
            item.Value.ThisShop = this;
        }
    }
}

