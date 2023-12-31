﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
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
        Products.Clear();
        productSlots.Clear();

        Products = factory.Make();
        //products.Add(new ShopProduct(new Weapon("test", new BasicEquipments(0,0,0,EquipmentType.Weapon), new WeaponData(0,true,0,0))));

        SettingShopUI();
        foreach (var item in BtnUIPair)
        {
            item.Key.GetComponentInChildren<TMP_Text>().text = "";
            item.Value.ThisShop = this;
        }
    }
}

