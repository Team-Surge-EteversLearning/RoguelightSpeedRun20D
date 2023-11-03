﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class V_EquipShop : Shop
{
    public override void ResetShop()
    {  
        products.Clear();
        productSlots.Clear();

        EquipmentDataManager equipFactory = new EquipmentDataManager();
        products = equipFactory.Make("0,3,2");
        SettingShopUI();
        OpenShop();
    }
}
