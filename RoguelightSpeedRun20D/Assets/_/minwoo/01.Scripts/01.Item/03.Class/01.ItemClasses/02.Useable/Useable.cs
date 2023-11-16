using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

public class Useable : IProduct
{
    public Useable(int itemCode, int quantity)
    {
        ItemCode = itemCode;
        Quantity = quantity;
    }
    public int ItemCode { get; set; }

    private int quantity;
    public int Quantity { get => quantity; set => quantity = value; }
    string IProduct.key { get => ItemCode + "";  }

    public void Buy()
    {
        if (quantity < 1)
        {
            Debug.Log("Sold Out");
            return;
        }
            
        switch (ItemCode) 
        {
            case 0:
                DungeonItemManager.hpPotionNow += 1;
                break;
            case 1:
                DungeonItemManager.manaPotionNow += 1;
                break;
            case 2:
                DungeonItemManager.bombNow += 1;
                break;
            case 3:
                DungeonItemManager.barrierNow += 1;
                break;
        }
        Quantity -= 1;
    }


}

