using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Village : MonoBehaviour
{
    private Dictionary<string, Shop> nameShopPair = new Dictionary<string, Shop>();
    private Dictionary<Button, ShopUI> btnShopUIPair = new Dictionary<Button, ShopUI>();
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] TMP_Text cashTxt;

    public Dictionary<Button, ShopUI> BtnShopUIPair { get => btnShopUIPair; set => btnShopUIPair = value; }
    public Action resetUI;
    public static Action onBuy;
    private void Start()
    {
        CreateShop();
        OptionBtnConnect();
        SlotsReset();
        cashTxt.text = PlayerStatsManager.CashNow.ToString();
        onBuy += () => cashTxt.text = PlayerStatsManager.CashNow.ToString();
    }


    private void OptionBtnConnect()
    {
        Button[] allPanelBtns = optionPanel.GetComponentsInChildren<Button>();

        foreach (Button b in allPanelBtns)
        {
            b.onClick.AddListener(() => ResetTargetShops(b.gameObject.name));
            b.onClick.AddListener(ChangeUISet);
        }
    }

    private void CreateShop()
    {
        EquipShop v_EquipShop = new EquipShop(0, 3, 3);
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
        shopPanel.SetActive(false);
        Debug.Log(shopPanel.activeInHierarchy);
    }

    public void ChangeUISet()
    {
        Debug.Log($"{optionPanel.activeInHierarchy} / {shopPanel.activeInHierarchy}");
        optionPanel.SetActive(!optionPanel.activeInHierarchy);
        shopPanel.SetActive(!shopPanel.activeInHierarchy);
    }

    [ContextMenu("INIT")]
    public void ResetTargetShops(string name)
    {
        resetUI();
        nameShopPair[name].ResetShop();
    }
}
