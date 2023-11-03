using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Useable : IProduct
{
    public Useable(int itemCode)
    {
        ItemCode = itemCode;
    }

    public int ItemCode { get; set; }
    public void Buy()
    {
        switch (ItemCode) 
        {
            case 0:
                Debug.Log("체력포션 구매");
                break;
            case 1:
                Debug.Log("마나포션 구매");
                break;
            case 2:
                Debug.Log("폭탄 구매");
                break;
            case 3:
                Debug.Log("배리어 구매");
                break;
        }
    }
}

