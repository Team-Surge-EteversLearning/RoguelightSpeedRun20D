using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PlayerStatsManager : IProductMaker
{
    static string name;
    static int cashNow;

    static int hpMax;
    static int staminaMax;
    static int manaMax;
    static float speed;
    static float powerWeight;

    static int hpMaxPrice;
    static int staminaMaxPrice;
    static int manaMaxPrice;
    static int speedPrice;
    static int powerWeightPrice;
    //if buy() stat, Price is added

    static List<IProduct> stats;
    static Dictionary<IProduct, int> priceTable = new Dictionary<IProduct, int>();

    void Init()
    {
        //read PlayerData and it's stored in the variable
        stats = new List<IProduct>()
        {
            //Since each statProduct has only one type and does not have different information,
            //it was judged that it was okay to generate it every time.
            new MaxHp("MaxHp"), new MaxStamina("MaxStamina"),new MaxStamina("MaxStamina"), new PowerWeight("PowerWeight"), new Speed("Speed")
        };
        priceTable.Add(stats[0], hpMaxPrice);
        priceTable.Add(stats[1], staminaMaxPrice);
        priceTable.Add(stats[2], manaMaxPrice);
        priceTable.Add(stats[3], speedPrice);
        priceTable.Add(stats[4], powerWeightPrice);
    }

    public void AddPrice(int index, int weightValue)
    {
        priceTable[stats[index]] += weightValue;
    }

    public List<ShopProduct> Make(string info)
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

