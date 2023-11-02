using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class V_EquipShop : Shop
{
    public override void InitShop(GameObject ui)
    {
        shopUI = ui;    
        products.Clear();
        productSlots.Clear();
        EquipmentDataManager equipFactory = new EquipmentDataManager();
        products = equipFactory.Make("0,3,2");
        //products.Add(new ShopProduct(new Weapon("test", new BasicEquipments(0,0,0,EquipmentType.Weapon), new WeaponData(0,true,0,0))));

        SettingShopUI();
        OpenShop();
    }
}
