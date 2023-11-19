using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerStatsManager : IProductMaker
{
    private static string name;
    private static int cashNow = 1000;
    private static int wareHouseCash = 1000;

    private static int hpMax = 10;
    private static int staminaMax = 10;
    private static int manaMax = 10;
    private static int speed = 0;
    private static int powerWeight = 10;

    private static int hpMaxPrice = 2;
    private static int staminaMaxPrice = 2;
    private static int manaMaxPrice = 2;
    private static int speedPrice = 2;
    private static int powerWeightPrice = 2;
    //if buy() stat, Price is added

    static List<IProduct> stats;
    static Dictionary<IProduct, int> priceTable = new Dictionary<IProduct, int>();
    static List<int> priceWeightList = new List<int>();
    #region property
    public static string Name { get => name; set => name = value; }
    public static int CashNow // InVillage
    { 
        get => cashNow;
        set 
        { 
            cashNow = value; 
        } 
    }
    public static int WareHouseCash { get => wareHouseCash; set => wareHouseCash = value; }
    public static int HpMax { get => hpMax; set => hpMax = value; }
    public static int StaminaMax { get => staminaMax; set => staminaMax = value; }
    public static int ManaMax { get => manaMax; set => manaMax = value; }
    public static int Speed { get => speed; set => speed = value; }
    public static int PowerWeight { get => powerWeight; set => powerWeight = value; }
    public static int HpMaxPrice { get => hpMaxPrice; set => hpMaxPrice = value; }
    public static int StaminaMaxPrice { get => staminaMaxPrice; set => staminaMaxPrice = value; }
    public static int ManaMaxPrice { get => manaMaxPrice; set => manaMaxPrice = value; }
    public static int SpeedPrice { get => speedPrice; set => speedPrice = value; }
    public static int PowerWeightPrice { get => powerWeightPrice; set => powerWeightPrice = value; }
    #endregion
    public void Init()
    {
        //read PlayerData and it's stored in the variable
        stats = new List<IProduct>()
        {
            //Since each statProduct has only one type and does not have different information,
            //it was judged that it was okay to generate it every time.
            new MaxHp("MaxHp", 0), new MaxStamina("MaxStamina", 1),new MaxMp("MaxMp", 2), new PowerWeight("PowerWeight", 3), new Speed("Speed", 4)
        };
        priceTable.Add(stats[0], HpMaxPrice);
        priceTable.Add(stats[1], StaminaMaxPrice);
        priceTable.Add(stats[2], ManaMaxPrice);
        priceTable.Add(stats[3], SpeedPrice);
        priceTable.Add(stats[4], PowerWeightPrice);
        PlayerSaveManager.LoadData("default");
    }

    public static void AddPrice(int index) 
    {
        priceTable[stats[index]] = (int)Math.Ceiling((double)priceTable[stats[index]] * 1.3);
        Debug.Log($"MaxHp : {hpMax} MaxStamina : {staminaMax} MaxMp : {manaMax} PowerWeight : {powerWeight} Speed : {speed}");
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

    public static void Set(string _name, int _cashNow, int _hpMax, int _staminaMax, int _manaMax, int _powerWeight)
    {
        name = _name;
        cashNow = _cashNow;
        hpMax = _hpMax;
        staminaMax = _staminaMax;
        manaMax = _manaMax;
        powerWeight = _powerWeight;
    }
}

