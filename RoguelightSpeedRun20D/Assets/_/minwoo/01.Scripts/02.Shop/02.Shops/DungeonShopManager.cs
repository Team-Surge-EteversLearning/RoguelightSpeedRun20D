using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonShopManager : MonoBehaviour
{
    public static DungeonShopManager Instance { get; private set; }
    [SerializeField] GameObject shopPanel;
    [SerializeField] TMP_Text cashTxt1;
    [SerializeField] TMP_Text cashTxt2;
    [SerializeField] Button exit;

    [SerializeField] PurchaseCompletePanelController purchaseCompletePanel;

    private Dictionary<Button, ShopUI> btnShopUIPair = new Dictionary<Button, ShopUI>();
    public Dictionary<Button, ShopUI> BtnShopUIPair { get => btnShopUIPair; set => btnShopUIPair = value; }
    public Action resetUI;

    List<Button> shopButtons = new List<Button>();
    public static Action<string> onBuy;

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
        cashTxt1.text = PlayerStatsManager.CashNow.ToString();
        onBuy += (string name) => cashTxt1.text = PlayerStatsManager.CashNow.ToString();
        cashTxt2.text = PlayerStatsManager.CashNow.ToString();
        onBuy += (string name) => cashTxt2.text = PlayerStatsManager.CashNow.ToString();
        onBuy += purchaseCompletePanel.ActiveAndDisable;

        exit.onClick.AddListener(() => { Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked; });
        exit.onClick.AddListener(() => shopPanel.gameObject.SetActive(false));
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
    public void CreateChestShop(GameObject go, int minTier, int maxTier, int amount)
    {
        ChestShop chestShop = new ChestShop(minTier,maxTier,amount);
        chestShop.InitShop(shopPanel, this);
        GOShopShopPair.Add(go, chestShop);
    }
    public void CreateInDungeonShop(GameObject go, int minTier, int maxTier, int amount, string type)
    {
        InDungeonShop inDungeonShop = new InDungeonShop(minTier, maxTier, amount, type);
        inDungeonShop.InitShop(shopPanel, this);
        GOShopShopPair.Add(go, inDungeonShop);
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

    public static void CursorToggle(bool flag)
    {
        Debug.Log(flag);
        Cursor.visible = flag;
        Cursor.lockState = flag ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
