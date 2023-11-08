using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StatShop : Shop
{

    public override void InitShop(GameObject ui, Village village)
    {
        base.InitShop(ui, village);
        factory = new PlayerStatsManager();
    }
    public override void ResetShop()
    {
        products.Clear();
        productSlots.Clear();

        products = factory.Make();
        //products.Add(new ShopProduct(new Weapon("test", new BasicEquipments(0,0,0,EquipmentType.Weapon), new WeaponData(0,true,0,0))));

        SettingShopUI(); 
    }
}

