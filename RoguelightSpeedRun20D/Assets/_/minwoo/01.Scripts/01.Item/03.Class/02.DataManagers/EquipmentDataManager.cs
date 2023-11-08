using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EquipmentDataManager : IProductMaker
{
    static Dictionary<string, BasicEquipments> unlocks = new Dictionary<string, BasicEquipments>();
    static Dictionary<string, BasicEquipments> locks = new Dictionary<string, BasicEquipments>();
    static Dictionary<string, WeaponData> weaponBasicTable = new Dictionary<string, WeaponData>();
    static Dictionary<string, ArmorData> armorBasicTable = new Dictionary<string, ArmorData>();
    static Dictionary<string, ShoesData> shoesBasicTable = new Dictionary<string, ShoesData>();

    SqlAccess sql;
    public void Init()
    {
        WeaponTableLoad();
        //ArmorTableLoad();
        //ShoesTableLoad();
        //read db
        //Categorize to each list
    }

    private void WeaponTableLoad()
    {
        sql = SqlAccess.GetAccess(Application.streamingAssetsPath + "/" + "test.db");
        sql.Open();
        sql.SqlRead("SELECT Item.name, EquipmentBasic.sellWhenClear, Item.price, Item.priceWeight, EquipmentBasic.modelIndex, Weapon.damage, Weapon.isRangeAttack, Weapon.size, Weapon.coolTime FROM item JOIN Weapon ON item.name = Weapon.name JOIN EquipmentBasic ON item.name = EquipmentBasic.name;");

        while (sql.read && sql.dataReader.Read())
        {
            string currentName = sql.dataReader.GetValue(0).ToString();

            BasicEquipments basicEquipments = new BasicEquipments(sql.dataReader.GetInt32(1), sql.dataReader.GetInt32(2), sql.dataReader.GetInt32(3), EquipmentType.Weapon, sql.dataReader.GetInt32(4));
            WeaponData weaponData = new WeaponData(sql.dataReader.GetInt32(5), Convert.ToBoolean(sql.dataReader.GetInt32(6)), sql.dataReader.GetInt32(7), sql.dataReader.GetFloat(8));

            unlocks.Add(currentName, basicEquipments);
            weaponBasicTable.Add(currentName, weaponData);
        }
        sql.dataReader.Close();
        sql.ShutDown();
    }
    private void ArmorTableLoad()
    {
        sql = SqlAccess.GetAccess(Application.streamingAssetsPath + "/" + "test.db");
        sql.Open();
        sql.SqlRead("SELECT Item.name, EquipmentBasic.sellWhenClear, Item.price, Item.priceWeight, EquipmentBasic.modelIndex, Armor.maxHp, Armor.trapAvoid, Armor.maxMana, Armor.manaRegen FROM item JOIN Armor ON item.name = Weapon.name JOIN EquipmentBasic ON item.name = EquipmentBasic.name;");

        while (sql.read && sql.dataReader.Read())
        {
            string currentName = sql.dataReader.GetValue(0).ToString();
            BasicEquipments basicEquipments = new BasicEquipments(sql.dataReader.GetInt32(1), sql.dataReader.GetInt32(2), sql.dataReader.GetInt32(3), EquipmentType.Weapon, sql.dataReader.GetInt32(4));
            ArmorData armorData = new ArmorData(sql.dataReader.GetInt32(5), Convert.ToBoolean(sql.dataReader.GetInt32(6)), sql.dataReader.GetInt32(7), sql.dataReader.GetFloat(8));

            locks.Add(currentName, basicEquipments); 
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
            BasicEquipments basicEquipments = new BasicEquipments(sql.dataReader.GetInt32(1), sql.dataReader.GetInt32(2), sql.dataReader.GetInt32(3), EquipmentType.Weapon, sql.dataReader.GetInt32(4));
            ShoesData shoesData = new ShoesData(sql.dataReader.GetInt32(5), sql.dataReader.GetFloat(6), sql.dataReader.GetInt32(7), sql.dataReader.GetFloat(8));

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

        // If you put in the correct value, it's stored in the variable, or it's stored in zero
        tierMin = int.TryParse(infoSplit[0].Trim(), out int tempVal1) ? tempVal1 : 0;
        tierMax = int.TryParse(infoSplit[1].Trim(), out int tempVal2) ? tempVal2 : 0;
        quantity = int.TryParse(infoSplit[2].Trim(), out int tempVal3) ? tempVal3 : 0;
        Debug.Log(quantity);


        // Creaate Random keys in unlocks
        List<string> displayItemNames = GetRandomItem(quantity);
        //List for return
        List<ShopProduct> displayItemListWithPrice = new List<ShopProduct>();
        for (int i = 0; i < displayItemNames.Count; i++)
        {

            string key = displayItemNames[i]; //current Item's name
            switch (unlocks[key].Type) //Create another object by type and add it to the list
            {
                case EquipmentType.Armor:
                    Armor newArmor = new Armor(key, unlocks[key], armorBasicTable[key]); //constructor:Armor(string name, BasicEquipments basicData, ArmorData data)
                    displayItemListWithPrice.Add(new ShopProduct( newArmor, unlocks[key].Price));
                    break;
                case EquipmentType.Weapon:
                    Weapon newWeapon = new Weapon(key, unlocks[key], weaponBasicTable[key]);
                    displayItemListWithPrice.Add(new ShopProduct(newWeapon, unlocks[key].Price));
                    break;
                case EquipmentType.Shoes:
                    Shoes newShoes = new Shoes(key, unlocks[key], shoesBasicTable[key]);
                    displayItemListWithPrice.Add(new ShopProduct(newShoes, unlocks[key].Price));
                    break;
            }
        }
        return displayItemListWithPrice;
    }

    //return string list in unlocks.keys
    private List<string> GetRandomItem(int n)
    {
        Random random = new Random();
        List<string> values = unlocks.Keys.ToList(); //test
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
}