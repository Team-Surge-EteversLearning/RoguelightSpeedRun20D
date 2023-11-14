using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class DungeonItemManager : IProductMaker
{
    static readonly int hpPotionMax = 5;
    static readonly int manaPotionMax = 5;
    static readonly int bombMax = 1;
    static readonly int barrierMax = 1;

    static int hpPotionPrice = 10;
    static int manaPotionPrice = 10;
    static int bombPrice = 10;
    static int barrierPrice = 10;

    static int hpPotionPriceWeight = hpPotionPrice * 2;
    static int manaPotionPriceWeight = manaPotionPrice * 2;
    static int bombPriceWeight = bombPrice * 2;
    static int barrierPriceWeight = barrierPrice * 2;

    static List<int> priceList = new List<int>()
    {
        hpPotionPrice, manaPotionPrice, bombPrice, barrierPrice
    };
    static List<int> priceWeightList;

    static int hpPotionShopNow;
    static int manaPotionShopNow;
    static int bombShopNow;
    static int barrierShopNow;

    public static int hpPotionNow;
    public static int manaPotionNow;
    public static int bombNow;
    public static int barrierNow;

    public static int HpPotionShopNow { get => hpPotionShopNow; set => hpPotionShopNow = value; }
    public static int ManaPotionShopNow { get => manaPotionShopNow; set => manaPotionShopNow = value; }
    public static int BombShopNow { get => bombShopNow; set => bombShopNow = value; }
    public static int BarrierShopNow { get => barrierShopNow; set => barrierShopNow = value; }

    public void InitPrcieTable()
    {
        //read PriceData on DB
        //priceList Init
        priceList = new List<int>()
        { hpPotionPrice, manaPotionPrice, bombPrice, barrierPrice};
        priceWeightList = new List<int>()
        { hpPotionPriceWeight, manaPotionPriceWeight, bombPriceWeight, barrierPriceWeight };
    }

    public void ResetShop()
    {
        HpPotionShopNow = 0;
        ManaPotionShopNow = 0;
        BombShopNow = 0;
        BarrierShopNow = 0;
    }
    /// <summary>
    /// input int4 + place (hpPotionQuantity, mpPotionQuantity, bombQuantity, barrierQuantity, D or V)
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public List<ShopProduct> Make(string info)
    {
        List<ShopProduct> displayItemListWithPrice = new List<ShopProduct>();
        List<int> itemList = new List<int>();
        List<int> priceTable;
        string[] infoSplit = info.Split(',');

        // If you put in the correct value, it's stored in the variable, or it's stored in zero
        HpPotionShopNow = int.TryParse(infoSplit[0].Trim(), out int tempVal1) ? tempVal1 : 0;
        Mathf.Clamp(HpPotionShopNow, 0, hpPotionMax);
        itemList.Add(HpPotionShopNow);

        ManaPotionShopNow = int.TryParse(infoSplit[1].Trim(), out int tempVal2) ? tempVal2 : 0;
        Mathf.Clamp(ManaPotionShopNow, 0, manaPotionMax);
        itemList.Add(ManaPotionShopNow);

        BombShopNow = int.TryParse(infoSplit[2].Trim(), out int tempVal3) ? tempVal3 : 0;
        Mathf.Clamp(BombShopNow, 0, bombMax);
        itemList.Add(BombShopNow);

        BarrierShopNow = int.TryParse(infoSplit[3].Trim(), out int tempVal4) ? tempVal4 : 0;
        Mathf.Clamp(BarrierShopNow, 0, barrierMax);
        itemList.Add(BarrierShopNow);

        if (infoSplit[4].First() == 'D' || infoSplit[4].First() == 'd')
        {
            priceTable = priceWeightList;
        }
        else
            priceTable = priceList;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != 0) //quantity 0 product not create 
            {
                Useable newUse = new Useable(i, itemList[i]);
                ShopProduct product = new ShopProduct(newUse, priceTable[i]);
                displayItemListWithPrice.Add(product);
            }
        }
        return displayItemListWithPrice;
    }

}
