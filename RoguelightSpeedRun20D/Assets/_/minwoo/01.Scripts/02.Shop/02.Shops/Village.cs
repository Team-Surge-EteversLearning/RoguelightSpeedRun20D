using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Village : MonoBehaviour
{
    private Dictionary<string, Shop> nameShopPair = new Dictionary<string, Shop>();
    private Dictionary<Button, ShopUI> btnShopUIPair = new Dictionary<Button, ShopUI>();
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] TMP_Text cashTxt;
    [SerializeField] PurchaseCompletePanelController purchaseCompletePanel;
    [SerializeField] Button startBtn;
    [SerializeField] TMP_Text descriptionTxt;
    
    [SerializeField] TMP_Text shopKeeperTxt;

    [SerializeField] GameObject Outfit;

    public Dictionary<Button, ShopUI> BtnShopUIPair { get => btnShopUIPair; set => btnShopUIPair = value; }
    public Action resetUI;
    public static Action<string> onBuy;
    private void Start()
    {
        onBuy = null;
        PlayerStatsManager.WareHouseCash += PlayerStatsManager.CashNow;
        PlayerStatsManager.CashNow = 0;
        purchaseCompletePanel = FindObjectOfType<PurchaseCompletePanelController>(true);
        //PlayerSaveManager.SaveData("default", PlayerStatsManager.WareHouseCash, PlayerStatsManager.HpMax, PlayerStatsManager.StaminaMax, PlayerStatsManager.ManaMax, PlayerStatsManager.PowerWeight, PlayerSM.skill1Index, PlayerSM.skill2Index, PlayerSaveManager.WrappingUnlocks());
        shopPanel.SetActive(false);
 
        CreateShop();
        OptionBtnConnect();
        SlotsReset();
        cashTxt.text = PlayerStatsManager.WareHouseCash.ToString();
        onBuy += (string name) => cashTxt.text = PlayerStatsManager.WareHouseCash.ToString();
        onBuy += purchaseCompletePanel.ActiveAndDisable;
        startBtn.onClick.AddListener(DungeonInsert);
    }

    private void DungeonInsert()
    {
        SceneManager.LoadScene(1);
    }

    private void OptionBtnConnect()
    {
        Button[] allPanelBtns = optionPanel.GetComponentsInChildren<Button>(true);

        foreach (Button b in allPanelBtns)
        {
            b.onClick.AddListener(ToggleShopUI);
            b.onClick.AddListener(() => ResetTargetShops(b.gameObject.name));
        }
    }
    private void CreateShop()
    {
        EquipShop v_EquipShop = new EquipShop(0, 5, 10);
        nameShopPair.Add("v_EquipShop", v_EquipShop);
        nameShopPair["v_EquipShop"].InitShop(shopPanel, this);

        PotionShop v_PotionShop = new PotionShop("v");
        nameShopPair.Add("v_PotionShop", v_PotionShop);
        nameShopPair["v_PotionShop"].InitShop(shopPanel, this);

        StatShop statShop = new StatShop();
        nameShopPair.Add("statShop", statShop);
        nameShopPair["statShop"].InitShop(shopPanel, this);

        SkillShop skillShop = new SkillShop();
        nameShopPair.Add("skillShop", skillShop);
        nameShopPair["skillShop"].InitShop(shopPanel, this);
    }
    private void SlotsReset()
    {
        Button[] allProductSlots = shopPanel.GetComponentsInChildren<Button>(true);
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

    public void ToggleShopUI()
    {
        shopPanel.SetActive(!shopPanel.activeInHierarchy);
        Outfit.SetActive(!shopPanel.activeInHierarchy);
        optionPanel.SetActive(!optionPanel.activeInHierarchy);
        if (shopPanel.activeInHierarchy)
            shopKeeperTxt.text = "버튼을 눌러 원하는 상품을 구매해 보게.";
        else if (!shopPanel.activeInHierarchy)
            shopKeeperTxt.text = "모험을 떠나기 전 오른쪽 상점을 통해 아이템과\r\n스탯, 스킬을 구매해 보게";
    }

    [ContextMenu("INIT")]
    public void ResetTargetShops(string name)
    {
        resetUI();
        nameShopPair[name].ResetShop();
    }

}
