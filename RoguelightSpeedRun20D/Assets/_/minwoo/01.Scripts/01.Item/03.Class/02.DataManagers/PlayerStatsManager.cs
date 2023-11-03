using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerStatsManager : IProductMaker
{
    static string name;
    static int cashNow;

    static int hpMax;
    static int staminaMax;
    static int manaMax;
    static float speed;
    static float powerWeight;

    static int hpMaxPrice = 2;
    static int staminaMaxPrice = 2;
    static int manaMaxPrice = 2;
    static int speedPrice = 2;
    static int powerWeightPrice = 2;
    //if buy() stat, Price is added

    static List<IProduct> stats;
    static Dictionary<IProduct, int> priceTable = new Dictionary<IProduct, int>();
    static List<int> priceWeightList = new List<int>();
    public void Init()
    {
        //read PlayerData and it's stored in the variable
        stats = new List<IProduct>()
        {
            //Since each statProduct has only one type and does not have different information,
            //it was judged that it was okay to generate it every time.
            new MaxHp("MaxHp", 0), new MaxStamina("MaxStamina", 1),new MaxStamina("MaxMp", 2), new PowerWeight("PowerWeight", 3), new Speed("Speed", 4)
        };
        priceTable.Add(stats[0], hpMaxPrice);
        priceTable.Add(stats[1], staminaMaxPrice);
        priceTable.Add(stats[2], manaMaxPrice);
        priceTable.Add(stats[3], speedPrice);
        priceTable.Add(stats[4], powerWeightPrice);
    }

    public static void AddPrice(int index) 
    {
        priceTable[stats[index]] = (int)Math.Ceiling((double)priceTable[stats[index]] * 1.3);
        Debug.Log(priceTable[stats[index]]);
    }
    /// <summary>
    /// you dont need input info
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public List<ShopProduct> Make(string info = "")
    {
        List<ShopProduct> displayItemListWithPrice = new List<ShopProduct>();

        for (int i = 0; i < 5; i++)
        {
            IProduct newStat = stats[i];
            ShopProduct product = new ShopProduct(newStat, priceTable[newStat]);
            displayItemListWithPrice.Add(product);
        }
        //return to shop
        return displayItemListWithPrice;
    }
}

