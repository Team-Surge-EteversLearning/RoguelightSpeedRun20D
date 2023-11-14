using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[Serializable]
public class ShopUI
{
    private Image productImage;
    private IProduct product;
    private ShopProduct s_product;
    private Button productButton;
    private int price;
    private Shop thisShop;

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
            if (productButton.gameObject.GetComponent<EventTrigger>() != null)
                UnityEngine.Object.Destroy(productButton.gameObject.GetComponent<EventTrigger>());
            Display();
        }
    }
    public Button ProductButton { get => productButton; set => productButton = value; }
    public Shop ThisShop { get => thisShop; set => thisShop = value; }
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
        Village.onBuy?.Invoke();
        DungeonShopManager.onBuy?.Invoke();
        if(productButton.gameObject.GetComponent<EventTrigger>() != null)
            UnityEngine.Object.Destroy(productButton.gameObject.GetComponent<EventTrigger>());
        return;
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
            productButton.onClick.AddListener(() => thisShop.Products.Remove(this.SProduct));
           
            EventTrigger eventTrigger = productButton.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((eventData) => { OnPointEnterProduct(productButton, equipment); });
            eventTrigger.triggers.Add(pointerEnterEntry);

            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerExitEntry.callback.AddListener((eventData) => { OnPointExitProduct(); });
            eventTrigger.triggers.Add(pointerExitEntry);
            return equipment.Name;
        }
        else if (product.GetType() == typeof(Useable))
        {
            Useable useable = (Useable)product;
            productButton.GetComponentInChildren<TMP_Text>().text = useable.Quantity.ToString();
            productButton.onClick.AddListener(() => productButton.GetComponentInChildren<TMP_Text>().text = useable.Quantity.ToString());
            return useable.ItemCode.ToString();
        }
        else if (statTypes.Contains(product.GetType()))
        {
            Stat stat = (Stat)product;
            productButton.GetComponentInChildren<TMP_Text>().text = "";
            return stat.Name;
        }
        else
        {
            ActiveSkill activeSkill = (ActiveSkill)product;
            productButton.GetComponentInChildren<TMP_Text>().text = "";
            return "RandomSkillBook";
        }
    }

    public static void OnPointEnterProduct(Button button, Equipment equip)
    {
        switch(equip)
        {
            case Weapon weapon:
                DescriptionController.onDescription?.Invoke($"{weapon.Name} ( +{weapon.Tier})\nDamage: {weapon.Damage}\nCoolTime: {weapon.Cooltime}", weapon.usableOptions);
                break;

        }
    }
    private void OnPointExitProduct()
    {
        DescriptionController.onDescriptionComplete?.Invoke();
    }

}
