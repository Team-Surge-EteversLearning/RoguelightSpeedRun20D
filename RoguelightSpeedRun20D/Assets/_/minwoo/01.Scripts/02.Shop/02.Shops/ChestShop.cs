using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestShop : Shop
{
    private string range;


    public override void InitShop(GameObject ui, DungeonShopManager dsManager)
    {
        base.InitShop(ui, dsManager);
        factory = new EquipmentDataManager();

        Products.Clear();
        Products = factory.Make(range);

        ShopProduct product = products[0];
        product.Price = 0;
        products[0] = product;
    }
    public ChestShop(int minTier, int maxTier, int amount)
    {
        this.range = $"{minTier},{maxTier},{amount}";
    }

    public override void ResetShop()
    {
        productSlots.Clear();

        SettingShopUI();

        foreach (var item in BtnUIPair)
        {
            item.Key.GetComponentInChildren<TMP_Text>().text = "";
            item.Value.ThisShop = this;
        }
    }
}
