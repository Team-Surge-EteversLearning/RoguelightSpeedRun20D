using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    private Village village;

    public GameObject ShopUI { get => shopUIGO; set => shopUIGO = value; }

    public virtual void InitShop(GameObject ui, Village village)
    {
        this.shopUIGO = ui;
        this.village = village;
        BtnUIPair = village.BtnShopUIPair;
    }

    abstract public void ResetShop(); //now setui afterReset, but Ingame, reset just one time.
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
            BtnUIPair[productSlots[i]].SProduct = products[i];
        }
        OpenShop();
    }
}
