using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonShopManager : MonoBehaviour
{
    public static DungeonShopManager Instance { get; private set; }
    [SerializeField] GameObject shopPanel;
    [SerializeField] TMP_Text cashTxt;

    private Dictionary<Button, ShopUI> btnShopUIPair = new Dictionary<Button, ShopUI>();
    public Dictionary<Button, ShopUI> BtnShopUIPair { get => btnShopUIPair; set => btnShopUIPair = value; }
    public Action resetUI;

    List<Button> shopButtons = new List<Button>();
    public static Action onBuy;

    private EquipmentDataManager dungeonEquipmentFactroy = new EquipmentDataManager();
    private DungeonItemManager dungeonPotionFactory = new DungeonItemManager();

    public  Dictionary<GameObject, Shop> GOShopShopPair = new Dictionary<GameObject, Shop>();
    public  Dictionary<GameObject, List<ShopProduct>> DungeonShopSProductPair = new Dictionary<GameObject, List<ShopProduct>>();


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Start()
    {
        InitSlot();
        SlotsReset();
        cashTxt.text = PlayerStatsManager.CashNow.ToString();
        onBuy += () => cashTxt.text = PlayerStatsManager.CashNow.ToString();
    }

    private void InitSlot()
    {
        Button[] buttons = shopPanel.GetComponentsInChildren<Button>();
        foreach (Button b in buttons) 
        {
            shopButtons.Add(b);
            b.onClick.RemoveAllListeners();
        }
    }
    public void CreateShop(GameObject go, int minTier, int maxTier, int amount)
    {
        ChestShop chestShop = new ChestShop(minTier,maxTier,amount);
        chestShop.InitShop(shopPanel, this);
        GOShopShopPair.Add(go, chestShop);
    }

    private void SlotsReset()
    {
        Button[] allProductSlots = shopPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < allProductSlots.Length; i++)
        {
            if (allProductSlots[i].name == "Exit")
                continue;
            BtnShopUIPair.Add(allProductSlots[i], new ShopUI(allProductSlots[i]));
        }
        resetUI = () =>
        {
            for (int i = 0; i < allProductSlots.Length; i++)
            {
                if (allProductSlots[i].name == "Exit")
                    continue;
                allProductSlots[i].GetComponentsInChildren<Image>()[1].sprite = TestDB.instance.iconSet.GetIcon("Default");
                allProductSlots[i].onClick.RemoveAllListeners();
                allProductSlots[i].gameObject.SetActive(false);
            }
        };
    }
    public void ResetTargetShops(GameObject GO)
    {
        resetUI();
        GOShopShopPair[GO].ResetShop();
    }
}
