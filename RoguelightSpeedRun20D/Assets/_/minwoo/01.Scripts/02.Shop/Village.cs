using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    [SerializeField]List<Shop> shops = new List<Shop>();
    [SerializeField] GameObject targetUI;
    private void Awake()
    {
        InitShops();
    }
    public void InitShops()
    {
        V_EquipShop v_EquipShop = new V_EquipShop();
        shops.Add(v_EquipShop);
        print(shops.Count);
        shops[0].InitShop(targetUI);
    }
}
