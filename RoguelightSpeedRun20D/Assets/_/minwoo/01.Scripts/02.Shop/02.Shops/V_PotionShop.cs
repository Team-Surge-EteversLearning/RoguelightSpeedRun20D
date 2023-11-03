using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class V_PotionShop : Shop
{
    public override void ResetShop()
    {
        products.Clear();
        productSlots.Clear();

        DungeonItemManager potionFactory = new DungeonItemManager();
        products = potionFactory.Make("5,5,0,0,v");
        //products.Add(new ShopProduct(new Weapon("test", new BasicEquipments(0,0,0,EquipmentType.Weapon), new WeaponData(0,true,0,0))));

        SettingShopUI();
        OpenShop();
    }
}
