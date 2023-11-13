using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Shop
{
    protected IProductMaker factory;
    protected List<ShopProduct> products = new List<ShopProduct>();
    protected List<Button> productSlots = new List<Button>();
    protected GameObject shopUIGO;
    protected Dictionary<Button, ShopUI> BtnUIPair = new Dictionary<Button, ShopUI>();

    public GameObject ShopUIGO { get => shopUIGO; set => shopUIGO = value; }
    public List<ShopProduct> Products { get => products; set => products = value; }

    public virtual void InitShop(GameObject ui, Village village)
    {
        this.shopUIGO = ui;
        BtnUIPair = village.BtnShopUIPair;
    }

    public virtual void InitShop(GameObject ui, DungeonShopManager dsManager)
    {
        this.shopUIGO = ui;
        BtnUIPair = dsManager.BtnShopUIPair;
    }

    abstract public void ResetShop(); //now setui afterReset, but Ingame, reset just one time.
    protected void OpenShop()
    {
        shopUIGO.SetActive(true);
    }

    protected void SettingShopUI()
    {
        Button[] allProductSlots = ShopUIGO.GetComponentsInChildren<Button>(true);
        Debug.Log(allProductSlots.Length);
        for (int i = 0; i < Products.Count; i++)
        {
            if (allProductSlots[i].name == "Exit")
                continue;
            //change slots image and txt
            productSlots.Add(allProductSlots[i]);
            productSlots[i].gameObject.SetActive(true);
            BtnUIPair[productSlots[i]].SProduct = Products[i];
        }
    }

}
