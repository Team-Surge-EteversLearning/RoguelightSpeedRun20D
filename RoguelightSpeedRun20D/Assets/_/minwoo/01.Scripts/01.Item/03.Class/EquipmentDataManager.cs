using System;
using System.Collections.Generic;
using System.Linq;

public class EquipmentDataManager : IProductMaker
{
    static Dictionary<string, BasicEquipments> unlocks;
    static Dictionary<string, BasicEquipments> locks;
    static Dictionary<string, WeaponData> weaponBasicTable;
    static Dictionary<string, ArmorData> armorBasicTable;
    static Dictionary<string, ShoesData> shoesBasicTable;

    void Init()
    {
        //read db
        //Categorize to each list
    }

    public List<ShopProduct> Make(string info)
    {
        string[] infoSplit = info.Split(',');

        int tierMin, tierMax, quantity;

        // If you put in the correct value, it's stored in the variable, or it's stored in zero
        tierMin = int.TryParse(infoSplit[0].Trim(), out int tempVal1) ? tempVal1 : 0;
        tierMax = int.TryParse(infoSplit[1].Trim(), out int tempVal2) ? tempVal2 : 0;
        quantity = int.TryParse(infoSplit[2].Trim(), out int tempVal3) ? tempVal3 : 0;

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
                    displayItemListWithPrice.Add(new ShopProduct(unlocks[key].Price, newArmor));
                    break;
                case EquipmentType.Weapon:
                    Weapon newWeapon = new Weapon(key, unlocks[key], weaponBasicTable[key]);
                    displayItemListWithPrice.Add(new ShopProduct(unlocks[key].Price, newWeapon));
                    break;
                case EquipmentType.Shoes:
                    Shoes newShoes = new Shoes(key, unlocks[key], shoesBasicTable[key]);
                    displayItemListWithPrice.Add(new ShopProduct(unlocks[key].Price, newShoes));
                    break;
            }
        }
        return displayItemListWithPrice;
    }

    //return string list in unlocks.keys
    private static List<string> GetRandomItem(int n)
    {
        Random random = new Random();
        List<string> values = unlocks.Keys.ToList();

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