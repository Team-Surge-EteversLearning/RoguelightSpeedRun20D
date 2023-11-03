using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StatShop : Shop
{
    public override void ResetShop()
    {
        products.Clear();
        productSlots.Clear();

        PlayerStatsManager statFactory = new PlayerStatsManager();
        products = statFactory.Make();
        //products.Add(new ShopProduct(new Weapon("test", new BasicEquipments(0,0,0,EquipmentType.Weapon), new WeaponData(0,true,0,0))));

        SettingShopUI();
        OpenShop();
    }
}

