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
using UnityEngine.InputSystem;

[Serializable]
public class ShopUI
{
    private Image productImage;
    private IProduct product;
    private ShopProduct s_product;
    private Button productButton;
    private int price;
    private Shop thisShop;

    private string name;

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
        if (thisShop.GetType() == typeof(ChestShop) || thisShop.GetType() == typeof(InDungeonShop)) // in dun
        {
            if (PlayerStatsManager.CashNow < price)
            {
                Debug.Log("Not Enough");
                return;
            }
            if (product.GetType() == typeof(Useable))
            {
                Useable useable = (Useable)product;
                if (useable.Quantity > 0)
                {
                    DungeonShopManager.onBuy?.Invoke(name);
                    PlayerStatsManager.CashNow -= price;
                    product.Buy();
                }
            }
            else
            {
                DungeonShopManager.onBuy?.Invoke(name);
                product.Buy();
                PlayerStatsManager.CashNow -= price;
            }
        }
        else                                                                                         // in vill
        {
            if (PlayerStatsManager.WareHouseCash < price)
            {
                Debug.Log("Not Enough");
                return;
            }
            if (product.GetType() == typeof(Useable))
            {
                Useable useable = (Useable)product;
                if (useable.Quantity > 0)
                {
                    Village.onBuy?.Invoke(name);
                    PlayerStatsManager.WareHouseCash -= price;
                    product.Buy();
                }
            }
            else
            {
                Village.onBuy?.Invoke(name);
                product.Buy();
                PlayerStatsManager.WareHouseCash -= price;
            }
        }
        DescriptionController.onDescriptionComplete?.Invoke();
        if (productButton.gameObject.GetComponent<EventTrigger>() != null && product is Equipment)
        {
            UnityEngine.Object.Destroy(productButton.gameObject.GetComponent<EventTrigger>());
        }

        if (price == 0)
            if (QuestSystem.currentQuests != null)
                foreach (Quest quest in QuestSystem.currentQuests)
                    if (quest.Key == product.key)
                        ((CollectItemQuest)quest).UpdateCurrentCount(1);
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
        EventTrigger eventTrigger = productButton.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        eventTrigger.triggers.Add(pointerEnterEntry);

        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((eventData) => { OnPointExitProduct(); });
        eventTrigger.triggers.Add(pointerExitEntry);
        if (equipTypes.Contains(product.GetType()))
        {
            Equipment equipment = (Equipment)product;
            productButton.onClick.AddListener(() => thisShop.Products.Remove(this.SProduct));
            pointerEnterEntry.callback.AddListener((eventData) => { OnPointEnterProduct(productButton, equipment); });
            name = equipment.Name;
            return equipment.Name;
        }
        else if (product.GetType() == typeof(Useable))
        {
            Useable useable = (Useable)product;
            productButton.GetComponentInChildren<TMP_Text>().text = useable.Quantity.ToString();
            productButton.onClick.AddListener(() => productButton.GetComponentInChildren<TMP_Text>().text = useable.Quantity.ToString());
            pointerEnterEntry.callback.AddListener((eventData) => { OnPointEnterProduct(productButton, useable); });
            name = useable.ItemCode.ToString();
            return useable.ItemCode.ToString();
        }
        else if (statTypes.Contains(product.GetType()))
        {
            Stat stat = (Stat)product;
            productButton.GetComponentInChildren<TMP_Text>().text = "";
            pointerEnterEntry.callback.AddListener((eventData) => { OnPointEnterProduct(productButton, stat); });
            name = stat.Name;
            return stat.Name;
        }
        else
        {
            ActiveSkill activeSkill = (ActiveSkill)product;
            productButton.GetComponentInChildren<TMP_Text>().text = "";
            productButton.onClick.AddListener(() => Debug.LogWarning(thisShop));
            productButton.onClick.AddListener(() => thisShop.ResetShop());
            pointerEnterEntry.callback.AddListener((eventData) => { OnPointEnterProduct(productButton, activeSkill); });
            name = activeSkill.Name;
            return "RandomSkillBook";
        }
    }

    public static void OnPointEnterProduct(Button button, IProduct product)
    {
        switch (product)
        {
            case Weapon weapon:
                DescriptionController.onDescriptionWithOpts?.Invoke($"{weapon.Name} ( +{weapon.Tier})\nDamage: {weapon.Damage}\nCoolTime: {weapon.Cooltime}", weapon.usableOptions);
                break;
            case Armor armor:
                DescriptionController.onDescriptionWithOpts?.Invoke($"{armor.Name} ( +{armor.Tier})\nMaxHp: {armor.MaxHp}\nMaxMana: {armor.MaxMana}\nManaRegen: {armor.ManaRegen}", armor.usableOptions);
                break;
            case Useable useable:
                string description = "";
                if (useable.ItemCode == 0)
                    description = "HpPotion";
                else if (useable.ItemCode == 1)
                    description = "ManaPotion";
                else if (useable.ItemCode == 2)
                    description = "Bomb";
                else if (useable.ItemCode == 3)
                    description = "Barrier";
                DescriptionController.onDescription?.Invoke(description);
                break;
            case Stat stat:
                DescriptionController.onDescription?.Invoke(stat.description);
                break;
            case ActiveSkill activeSkill:
                DescriptionController.onDescription?.Invoke("RandomSkillBook");
                break;

        }
    }
    public static void OnPointEnterProduct(string description)
    {
        DescriptionController.onDescription?.Invoke(description);
    }
    private void OnPointExitProduct()
    {
        DescriptionController.onDescriptionComplete?.Invoke();
    }

}
