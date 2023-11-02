using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public abstract class Shop
{
    protected IProductMaker factory;
    protected List<ShopProduct> products = new List<ShopProduct>();
    protected List<Button> productSlots = new List<Button>();
    protected GameObject shopUI;

    IProduct currentProduct;
    Text currentTxt;

    public GameObject ShopUI { get => shopUI; set => shopUI = value; }

    abstract public void InitShop(GameObject ui);

    //- 초기화, 리셋 기능
    //- ShopUI 생성 혹은 이미 존재하는거 끌어다 UI 초기화시킴

    protected void OpenShop()
    {
        ShopUI.SetActive(ShopUI.activeInHierarchy);
    }

    protected void SettingShopUI()
    {
        Button[] allProductSlots = ShopUI.GetComponentsInChildren<Button>();
        for (int i = 0; i < products.Count; i++)
        {
            //change slots image and txt
            allProductSlots[i].gameObject.SetActive(true);
            currentTxt = allProductSlots[i].GetComponentInChildren<Text>();
            currentProduct = products[i].Product;
            JudgeType();
            productSlots.Add(allProductSlots[i]);
        }
    }

    private void JudgeType()
    {
        var equipTypes = new[] { typeof(Armor), typeof(Shoes), typeof(Weapon) };
        var statTypes = new[] { typeof(MaxHp), typeof(MaxMp), typeof(MaxStamina), typeof(PowerWeight), typeof(Speed) };
        if (equipTypes.Contains(currentProduct.GetType()))
        {
            Equipment equipment = (Equipment)currentProduct;
            currentTxt.text = equipment.Name;
        }
        else if (currentProduct.GetType() == typeof(Useable))
        {
            Useable useable = (Useable)currentProduct;
            currentTxt.text = useable.ItemCode.ToString();
        }
        else if (statTypes.Contains(currentProduct.GetType()))
        {
            Stat stat = (Stat)currentProduct;
            currentTxt.text = stat.Name;
        }
        else if(currentProduct.GetType() == typeof(ActiveSkill))
        {
            ActiveSkill activeSkill = (ActiveSkill)currentProduct;
            currentTxt.text = "RandomSkillBook";
        }
    }
}
