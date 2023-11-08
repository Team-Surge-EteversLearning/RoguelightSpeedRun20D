using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

[Serializable]
public class ShopUI
{
    private Image productImage;
    private IProduct product;
    private ShopProduct s_product;
    private Button productButton;
    private int price;

    public Image ProductImage { get => productImage; set => productImage = value; }
    public ShopProduct SProduct
    {
        get => s_product;
        set
        {
            s_product = value;
            product = value.Product;
            price = value.Price;
            productButton.onClick.RemoveAllListeners();
            productButton.onClick.AddListener(CashCheckAndBuy);
            Display();
        }
    }
    public Button ProductButton { get => productButton; set => productButton = value; }

    public ShopUI(Button productButton)
    {
        this.ProductButton = productButton;
        this.ProductImage = productButton.GetComponentsInChildren<Image>()[1];
    }

    private void CashCheckAndBuy()
    {
        if (PlayerStatsManager.CashNow < price)
        {
            Debug.Log("µ·ºÎÁ·");
            return;
        }
        PlayerStatsManager.CashNow -= price;
        product.Buy();
        Debug.Log(PlayerStatsManager.CashNow);
    }
    private void Display()
    {
        ProductImage.sprite = TestDB.instance.iconSet.GetIcon(JudgeType());
    }
    private string JudgeType()
    {
        var equipTypes = new[] { typeof(Armor), typeof(Shoes), typeof(Weapon) };
        var statTypes = new[] { typeof(MaxHp), typeof(MaxMp), typeof(MaxStamina), typeof(PowerWeight), typeof(Speed) };
        if (equipTypes.Contains(product.GetType()))
        {
            Equipment equipment = (Equipment)product;
            Debug.Log(equipment.Name);
            return equipment.Name;
        }
        else if (product.GetType() == typeof(Useable))
        {
            Useable useable = (Useable)product;
            productButton.GetComponentInChildren<Text>().text = useable.Quantity.ToString();
            productButton.onClick.AddListener(() => productButton.GetComponentInChildren<Text>().text = useable.Quantity.ToString());
            Debug.Log(useable.ItemCode.ToString());
            return useable.ItemCode.ToString();
        }
        else if (statTypes.Contains(product.GetType()))
        {
            Stat stat = (Stat)product;
            Debug.Log(stat.Name);
            return stat.Name;
        }
        else
        {
            ActiveSkill activeSkill = (ActiveSkill)product;
            return "RandomSkillBook";
        }
    }
}
