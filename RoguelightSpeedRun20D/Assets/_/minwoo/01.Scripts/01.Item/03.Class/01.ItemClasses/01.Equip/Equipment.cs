
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
    private int tier;

    private BasicEquipments basicData;
    public BasicEquipments BasicData { get => basicData; set => basicData = value; }

    public string Name { get => name; set => name = value; }
    public int SellWhenClear { get => sellWhenClear; set => sellWhenClear = value; }
    public EquipmentType Type { get => type; set => type = value; }
    public int ModelIndex { get => modelIndex; set => modelIndex = value; }
    public int Tier { get => tier; set => tier = value; }

    public List<EquipmentOption> usableOptions;

    public readonly static List<EquipmentOption> weaponOptionPool = new List<EquipmentOption>
    { 
        new WeaponOpt_AddDamage0(),
        new WeaponOpt_AddDamage1(),
        new WeaponOpt_AddDamage2(),
        new WeaponOpt_AddDamage3(),
        new WeaponOpt_AddDamage4(),
        new WeaponOpt_ReduceCoolTime0(),
        new WeaponOpt_ReduceCoolTime1(),
        new WeaponOpt_ReduceCoolTime2(),
        new WeaponOpt_ReduceCoolTime3(),
        new WeaponOpt_ReduceCoolTime4()
    };
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