using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

[Serializable]
public class ShopUI
{
    private Image productImage;
    private IProduct product;
    private Button productButton;
    public UnityEvent onProductSelected;

    public Image ProductImage { get => productImage; set => productImage = value; }
    public IProduct Product
    {
        get => product;
        set
        {
            product = value;
            productButton.onClick.RemoveAllListeners();
            productButton.onClick.AddListener(product.Buy);
            Display();
        }
    }
    public Button ProductButton { get => productButton; set => productButton = value; }

    public ShopUI(Button productButton)
    {
        this.ProductButton = productButton;
        this.ProductImage = productButton.GetComponent<Image>();
    }

    private void Display()
    {
        ProductImage.sprite = TestDB.instance.iconSet.GetIcon(JudgeType());
    }
    private string JudgeType()
    {
        var equipTypes = new[] { typeof(Armor), typeof(Shoes), typeof(Weapon) };
        var statTypes = new[] { typeof(MaxHp), typeof(MaxMp), typeof(MaxStamina), typeof(PowerWeight), typeof(Speed) };
        if (equipTypes.Contains(Product.GetType()))
        {
            Equipment equipment = (Equipment)Product;
            return equipment.Name;
        }
        else if (Product.GetType() == typeof(Useable))
        {
            Useable useable = (Useable)Product;
            productButton.GetComponentInChildren<Text>().text = useable.Quantity.ToString();
            productButton.onClick.AddListener(() => productButton.GetComponentInChildren<Text>().text = useable.Quantity.ToString());
            return useable.ItemCode.ToString();
        }
        else if (statTypes.Contains(Product.GetType()))
        {
            Stat stat = (Stat)Product;
            return stat.Name;
        }
        else
        {
            ActiveSkill activeSkill = (ActiveSkill)Product;
            return "RandomSkillBook";
        }
    }
}
