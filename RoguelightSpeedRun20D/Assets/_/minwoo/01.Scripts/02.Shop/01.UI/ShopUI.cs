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
        Debug.Log(PlayerStatsManager.CashNow);
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
           
            EventTrigger eventTrigger = productImage.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((eventData) => { OnPointEnterProduct(productButton, equipment); });
            eventTrigger.triggers.Add(pointerEnterEntry);

            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerExitEntry.callback.AddListener((eventData) => { OnPointExitProduct(productButton, equipment); });
            eventTrigger.triggers.Add(pointerExitEntry);

            productButton.onClick.AddListener(() => UnityEngine.Object.Destroy(eventTrigger));

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
        DescriptionController.onDescription?.Invoke(equip.Name);
    }
    private void OnPointExitProduct(Button productButton, Equipment equipment)
    {
        DescriptionController.onDescriptionComplete?.Invoke();
    }

}
