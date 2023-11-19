
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
    string IProduct.key { get => Name;}

    public List<EquipmentOption> usableOptions;
    public readonly static List<EquipmentOption> weaponOptionPool = new List<EquipmentOption>
    {
        new WeaponOpt_AddDamage0(10),
        new WeaponOpt_AddDamage1(15),
        new WeaponOpt_AddDamage2(20),
        new WeaponOpt_AddDamage3(25),
        new WeaponOpt_AddDamage4(30),
        new WeaponOpt_ReduceCoolTime0(10),
        new WeaponOpt_ReduceCoolTime1(15),
        new WeaponOpt_ReduceCoolTime2(20),
        new WeaponOpt_ReduceCoolTime3(25),
        new WeaponOpt_ReduceCoolTime4(30),
        new WeaponOpt_EvilPower(20),
        new WeaponOpt_AngelPower(20),
    };
    public readonly static List<EquipmentOption> armorOptionPool = new List<EquipmentOption> 
    {
        new ArmorOpt_AddMaxHp0(10),
        new ArmorOpt_AddMaxHp1(15),
        new ArmorOpt_AddMaxHp2(20),
        new ArmorOpt_AddMaxMp0(10),
        new ArmorOpt_AddMaxMp1(15),
        new ArmorOpt_AddMaxMp2(20),
        new ArmorOpt_Special0(20),
        new ArmorOpt_Special1(20),
    };
    public readonly static List<EquipmentOption> shoesOptionPool = new List<EquipmentOption> { };

    public void Buy()
    {
        Button prvBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        prvBtn.onClick.RemoveAllListeners();
        prvBtn.GetComponentsInChildren<Image>()[1].sprite = TestDB.instance.iconSet.GetIcon("Default");
        if(!EquipmentDataManager.unlocks.ContainsKey(name))
            EquipmentDataManager.unlocks.Add(name, basicData);
        Equip();
    }

    public abstract void Equip();

    public GameObject MakeInGame(List<GameObject> modelInstance)
    {
        GameObject thisWeapon = modelInstance[ModelIndex];
        foreach (EquipmentOption option in usableOptions)
        {
            thisWeapon = option.MakeInGame(thisWeapon);
            this.SellWhenClear += option.sellWhenClear;
        }

        return thisWeapon;
    }

}