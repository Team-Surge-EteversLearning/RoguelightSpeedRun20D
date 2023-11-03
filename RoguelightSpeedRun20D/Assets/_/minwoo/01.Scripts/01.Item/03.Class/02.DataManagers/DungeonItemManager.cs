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

    static int hpPotionPrice;
    static int manaPotionPrice;
    static int bombPrice;
    static int barrierPrice;

    static List<int> priceList = new List<int>()
    {
        hpPotionPrice, manaPotionPrice, bombPrice, barrierPrice
    };
    static int hpPotionPriceWeight;
    static int manaPotionPriceWeight;
    static int bombPriceWeight;
    static int barrierPriceWeight;

    static List<int> priceWeightList;

    static int hpPotionNow;
    static int manaPotionNow;
    static int bombNow;
    static int barrierNow;

    public static int HpPotionNow { get => hpPotionNow; set => hpPotionNow = value; }
    public static int ManaPotionNow { get => manaPotionNow; set => manaPotionNow = value; }
    public static int BombNow { get => bombNow; set => bombNow = value; }
    public static int BarrierNow { get => barrierNow; set => barrierNow = value; }

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
        HpPotionNow = 0;
        ManaPotionNow = 0;
        BombNow = 0;
        BarrierNow = 0;
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
        HpPotionNow = int.TryParse(infoSplit[0].Trim(), out int tempVal1) ? tempVal1 : 0;
        Mathf.Clamp(HpPotionNow, 0, hpPotionMax);
        itemList.Add(HpPotionNow);

        ManaPotionNow = int.TryParse(infoSplit[1].Trim(), out int tempVal2) ? tempVal2 : 0;
        Mathf.Clamp(ManaPotionNow, 0, manaPotionMax);
        itemList.Add(ManaPotionNow);

        BombNow = int.TryParse(infoSplit[2].Trim(), out int tempVal3) ? tempVal3 : 0;
        Mathf.Clamp(BombNow, 0, bombMax);
        itemList.Add(BombNow);

        BarrierNow = int.TryParse(infoSplit[3].Trim(), out int tempVal4) ? tempVal4 : 0;
        Mathf.Clamp(BarrierNow, 0, barrierMax);
        itemList.Add(BarrierNow);

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
