using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PotionShop : Shop
{
    private string type;
    private DungeonItemManager potionFac;

    public PotionShop(string type)
    {
        this.type = type;
    }
    public override void InitShop(GameObject ui, Village village)
    {
        base.InitShop(ui, village);
        factory = new DungeonItemManager();
        potionFac = (DungeonItemManager)factory;
        potionFac.ResetShop();
        products.Clear();
        products = potionFac.Make($"5,5,0,0,{type}");
    }

    public override void ResetShop()
    {

        productSlots.Clear();
        //products.Add(new ShopProduct(new Weapon("test", new BasicEquipments(0,0,0,EquipmentType.Weapon), new WeaponData(0,true,0,0))));
        SettingShopUI();
    }
}
