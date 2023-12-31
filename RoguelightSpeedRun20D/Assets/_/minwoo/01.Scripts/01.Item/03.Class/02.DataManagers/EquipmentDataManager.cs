using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class EquipmentDataManager : IProductMaker
{
    public static Dictionary<string, BasicEquipments> unlocks = new Dictionary<string, BasicEquipments>();
    public static Dictionary<string, BasicEquipments> locks = new Dictionary<string, BasicEquipments>();
    public static Dictionary<string, WeaponData> weaponBasicTable = new Dictionary<string, WeaponData>();
    public static Dictionary<string, ArmorData> armorBasicTable = new Dictionary<string, ArmorData>();
    public static Dictionary<string, ShoesData> shoesBasicTable = new Dictionary<string, ShoesData>();

    SqlAccess sql;
    public void Init()
    {
        WeaponTableLoad();
        ArmorTableLoad();

        //ShoesTableLoad();
        //read db
        //Categorize to each list
        if (!unlocks.ContainsKey("armingSword"))
            unlocks.Add("armingSword", locks["armingSword"]);
        if (!unlocks.ContainsKey("pedora"))
            unlocks.Add("pedora", locks["pedora"]);
    }

    private void WeaponTableLoad()
    {
        sql = SqlAccess.GetAccess(Application.streamingAssetsPath + "/" + "GameData.db");
        sql.Open();
        sql.SqlRead("SELECT Item.name, EquipmentBasic.sellWhenClear, Item.price, Item.priceWeight, EquipmentBasic.modelIndex, Weapon.damage, Weapon.isRangeAttack, Weapon.size, Weapon.coolTime FROM item JOIN Weapon ON item.name = Weapon.name JOIN EquipmentBasic ON item.name = EquipmentBasic.name;");

        while (sql.read && sql.dataReader.Read())
        {
            string currentName = sql.dataReader.GetValue(0).ToString();

            BasicEquipments basicEquipments = new BasicEquipments(sql.dataReader.GetInt32(1), sql.dataReader.GetInt32(2), sql.dataReader.GetInt32(3), EquipmentType.Weapon, sql.dataReader.GetInt32(4));
            WeaponData weaponData = new WeaponData(sql.dataReader.GetInt32(5), Convert.ToBoolean(sql.dataReader.GetInt32(6)), sql.dataReader.GetFloat(7), sql.dataReader.GetFloat(8));

            if (!locks.ContainsKey(currentName))
                locks.Add(currentName, basicEquipments);

            if (!weaponBasicTable.ContainsKey(currentName))
                weaponBasicTable.Add(currentName, weaponData);
        }
        sql.dataReader.Close();
        sql.ShutDown();
    }
    private void ArmorTableLoad()
    {
        sql = SqlAccess.GetAccess(Application.streamingAssetsPath + "/" + "GameData.db");
        sql.Open();
        sql.SqlRead("SELECT Item.name, EquipmentBasic.sellWhenClear, Item.price, Item.priceWeight, EquipmentBasic.modelIndex, Armor.maxHp, Armor.trapAvoid, Armor.maxMana, Armor.manaRegen FROM item JOIN Armor ON item.name = Armor.name JOIN EquipmentBasic ON item.name = EquipmentBasic.name;");

        while (sql.read && sql.dataReader.Read())
        {

            string currentName = sql.dataReader.GetValue(0).ToString();
            BasicEquipments basicEquipments = new BasicEquipments(sql.dataReader.GetInt32(1), sql.dataReader.GetInt32(2), sql.dataReader.GetInt32(3), EquipmentType.Armor, sql.dataReader.GetInt32(4));
            ArmorData armorData = new ArmorData(sql.dataReader.GetInt32(5), Convert.ToBoolean(sql.dataReader.GetInt32(6)), sql.dataReader.GetInt32(7), sql.dataReader.GetFloat(8));

            if (!locks.ContainsKey(currentName))
                locks.Add(currentName, basicEquipments);
            if (!armorBasicTable.ContainsKey(currentName))
                armorBasicTable.Add(currentName, armorData);
        }
        sql.dataReader.Close();
        sql.ShutDown();

    }
    private void ShoesTableLoad()
    {
        sql = SqlAccess.GetAccess(Application.streamingAssetsPath + "/" + "test.db");
        sql.Open();
        sql.SqlRead("SELECT Item.name, EquipmentBasic.sellWhenClear, Item.price, Item.priceWeight, EquipmentBasic.modelIndex, Shoes.maxHp, Shoes.speed, Shoes.maxStamina, Shoes.staminaRegen FROM item JOIN Weapon ON item.name = Weapon.name JOIN EquipmentBasic ON item.name = EquipmentBasic.name;");

        while (sql.read && sql.dataReader.Read())
        {
            string currentName = sql.dataReader.GetValue(0).ToString();
            BasicEquipments basicEquipments = new BasicEquipments(sql.dataReader.GetInt32(1), sql.dataReader.GetInt32(2), sql.dataReader.GetInt32(3), EquipmentType.Shoes, sql.dataReader.GetInt32(4));
            ShoesData shoesData = new ShoesData(sql.dataReader.GetInt32(5), sql.dataReader.GetInt32(6), sql.dataReader.GetInt32(7), sql.dataReader.GetFloat(8));

            locks.Add(currentName, basicEquipments);
            shoesBasicTable.Add(currentName, shoesData);
        }
        sql.dataReader.Close();
        sql.ShutDown();
    }


    /// <summary>
    /// input int3 (tierMin, tierMax, quantity)
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public List<ShopProduct> Make(string info)
    {

        string[] infoSplit = info.Split(',');

        int tierMin, tierMax, quantity;
        string shopType;

        // If you put in the correct value, it's stored in the variable, or it's stored in zero
        tierMin = int.TryParse(infoSplit[0].Trim(), out int tempVal1) ? tempVal1 : 0;
        tierMax = int.TryParse(infoSplit[1].Trim(), out int tempVal2) ? tempVal2 : 0;
        quantity = int.TryParse(infoSplit[2].Trim(), out int tempVal3) ? tempVal3 : 0;
        shopType = infoSplit[3].Trim();
        Dictionary<string, BasicEquipments> pool;

        if (shopType == "D")
        {
            pool = locks;
        }
        else
        {
            pool = unlocks;
        }

        // Creaate Random keys in unlocks
        List<string> displayItemNames = GetRandomItem(quantity, pool);
        //List for return
        List<ShopProduct> displayItemListWithPrice = new List<ShopProduct>();
        for (int i = 0; i < displayItemNames.Count; i++)
        {
            int currentTier = UnityEngine.Random.Range(tierMin, tierMax + 1);
            string key = displayItemNames[i]; //current Item's name

            switch (pool[key].Type) //Create another object by type and add it to the list
            {
                case EquipmentType.Armor:
                    Armor newArmor = new Armor(key, pool[key], armorBasicTable[key], SelectRandomOption(pool[key].Type, currentTier), currentTier); //constructor:Armor(string name, BasicEquipments basicData, ArmorData data)
                    displayItemListWithPrice.Add(new ShopProduct(newArmor, pool[key].Price + SetPriceAddedOpt(newArmor.usableOptions)));
                    break;
                case EquipmentType.Weapon:
                    Weapon newWeapon = new Weapon(key, pool[key], weaponBasicTable[key], SelectRandomOption(pool[key].Type, currentTier), currentTier);
                    displayItemListWithPrice.Add(new ShopProduct(newWeapon, pool[key].Price + SetPriceAddedOpt(newWeapon.usableOptions)));
                    break;
                case EquipmentType.Shoes:
                    Shoes newShoes = new Shoes(key, pool[key], shoesBasicTable[key], SelectRandomOption(pool[key].Type, currentTier), currentTier);
                    displayItemListWithPrice.Add(new ShopProduct(newShoes, pool[key].Price + SetPriceAddedOpt(newShoes.usableOptions)));
                    break;
            }
        }
        return displayItemListWithPrice;
    }
    private List<EquipmentOption> SelectRandomOption(EquipmentType equipmentType, int tier)
    {
        List<EquipmentOption> currentEquipOpts = new List<EquipmentOption>();
        for (int i = 0; i < tier; i++)
        {
            switch (equipmentType)
            {
                case EquipmentType.Armor:
                    currentEquipOpts.Add(Equipment.armorOptionPool[UnityEngine.Random.Range(0, Equipment.armorOptionPool.Count)]);
                    break;
                case EquipmentType.Weapon:
                    currentEquipOpts.Add(Equipment.weaponOptionPool[UnityEngine.Random.Range(0, Equipment.weaponOptionPool.Count)]);
                    break;
                case EquipmentType.Shoes:
                    break;
            }
        }
        return currentEquipOpts;
    }
    private int SetPriceAddedOpt(List<EquipmentOption> opts)
    {
        int temp = 0;
        foreach (var item in opts)
        {
            temp += item.priceOffset;
        }
        return temp;
    }


    //return string list in unlocks.keys
    private List<string> GetRandomItem(int n, Dictionary<string, BasicEquipments> pool)
    {
        Random random = new Random();
        List<string> values = pool.Keys.ToList(); //test
                                                  // shffle List
        for (int i = values.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            var temp = values[i];
            values[i] = values[j];
            values[j] = temp;
        }
        // return
        return values.Take(n).ToList();
    }

    public static void Load(List<string> itemUnlock)
    {
        for (int i = 0; i < itemUnlock.Count; i++)
        {
            foreach (var item in locks)
            {
                if (item.Key == itemUnlock[i] && !unlocks.ContainsKey(item.Key))
                {
                    unlocks.Add(item.Key, item.Value);
                    break;
                }
            }
        }
    }
}