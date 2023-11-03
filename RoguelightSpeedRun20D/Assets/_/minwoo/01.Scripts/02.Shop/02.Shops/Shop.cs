using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public abstract class Shop
{
    protected IProductMaker factory;
    protected List<ShopProduct> products = new List<ShopProduct>();
    protected List<Button> productSlots = new List<Button>();
    protected GameObject shopUIGO;
    protected Dictionary<Button, ShopUI> BtnUIPair = new Dictionary<Button, ShopUI>();

    private Village village;

    public GameObject ShopUI { get => shopUIGO; set => shopUIGO = value; }

    public void InitShop(GameObject ui, Village village)
    {
        this.shopUIGO = ui;
        this.village = village;
        BtnUIPair = village.BtnShopUIPair;
    }

    abstract public void ResetShop();

    //- 초기화, 리셋 기능
    //- ShopUI 생성 혹은 이미 존재하는거 끌어다 UI 초기화시킴

    protected void OpenShop()
    {
        shopUIGO.SetActive(shopUIGO.activeInHierarchy);
    }

    protected void SettingShopUI()
    {
        Button[] allProductSlots = ShopUI.GetComponentsInChildren<Button>();
        for (int i = 0; i < products.Count; i++)
        {
            //change slots image and txt
            productSlots.Add(allProductSlots[i]);
            productSlots[i].gameObject.SetActive(true);
            BtnUIPair[productSlots[i]].Product = products[i].Product;
        }
    }
}
