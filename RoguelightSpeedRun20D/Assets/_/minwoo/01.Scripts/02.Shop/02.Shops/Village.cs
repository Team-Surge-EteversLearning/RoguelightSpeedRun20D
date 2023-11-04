using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Village : MonoBehaviour
{
    private Dictionary<string, Shop> nameShopPair = new Dictionary<string, Shop>();
    private Dictionary<Button, ShopUI> btnShopUIPair = new Dictionary<Button, ShopUI>();
    [SerializeField] GameObject targetUI;
    [SerializeField] string shopName;

    public Dictionary<Button, ShopUI> BtnShopUIPair { get => btnShopUIPair; set => btnShopUIPair = value; }

    public Action resetUI;
    private void Awake()
    {
        Button[] allProductSlots = targetUI.GetComponentsInChildren<Button>();
        for (int i = 0; i < allProductSlots.Length; i++)
        {
            BtnShopUIPair.Add(allProductSlots[i], new ShopUI(allProductSlots[i]));

        }
        resetUI = () =>
        {
            for (int i = 0; i < allProductSlots.Length; i++)
            {
                allProductSlots[i].image.sprite = TestDB.instance.iconSet.GetIcon("Default");
                allProductSlots[i].onClick.RemoveAllListeners();
            }
        };
        EquipShop v_EquipShop = new EquipShop(0, 3);
        nameShopPair.Add("v_EquipShop", v_EquipShop);
        nameShopPair["v_EquipShop"].InitShop(targetUI, this);

        PotionShop v_PotionShop = new PotionShop("v");
        nameShopPair.Add("v_PotionShop", v_PotionShop);
        nameShopPair["v_PotionShop"].InitShop(targetUI, this);

        StatShop statShop = new StatShop();
        nameShopPair.Add("statShop", statShop);
        nameShopPair["statShop"].InitShop(targetUI, this);

        SkillShop skillShop = new SkillShop();
        nameShopPair.Add("skillShop", skillShop);
        nameShopPair["skillShop"].InitShop(targetUI, this);
    }

    [ContextMenu("INIT")]
    public void ResetTargetShops()
    {
        resetUI();
        nameShopPair[shopName].ResetShop();
    }
}
