
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public abstract class Equipment : IProduct
{
    private string name;
    private int sellWhenClear;
    private EquipmentType type;
    private int modelIndex;

    private BasicEquipments basicData;
    public BasicEquipments BasicData { get => basicData; set => basicData = value; }

    public string Name { get => name; set => name = value; }
    public int SellWhenClear { get => sellWhenClear; set => sellWhenClear = value; }
    public EquipmentType Type { get => type; set => type = value; }
    public int ModelIndex { get => modelIndex; set => modelIndex = value; }

    public List<EquipmentOption> usableOptions;

    public readonly static List<EquipmentOption> weaponOptionPool = new List<EquipmentOption> { };
    public readonly static List<EquipmentOption> armorOptionPool = new List<EquipmentOption> { };
    public readonly static List<EquipmentOption> shoesOptionPool = new List<EquipmentOption> { };

    public void Buy()
    {
        Button prvBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        prvBtn.onClick.RemoveAllListeners();
        prvBtn.GetComponentsInChildren<Image>()[1].sprite = TestDB.instance.iconSet.GetIcon("Default");
        Equip();
    }

    public abstract void Equip();

    public GameObject MakeInGame(List<GameObject> modelInstance)
    {
        GameObject thisWeapon = modelInstance[ModelIndex];
        foreach (EquipmentOption option in usableOptions)
            thisWeapon = option.MakeInGame(thisWeapon);

        return thisWeapon;
    }
}