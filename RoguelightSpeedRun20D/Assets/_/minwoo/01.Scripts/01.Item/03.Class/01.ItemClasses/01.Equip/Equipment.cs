
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

    public string Name { get => name; set => name = value; }
    public int SellWhenClear { get => sellWhenClear; set => sellWhenClear = value; }
    public EquipmentType Type { get => type; set => type = value; }
    public int ModelIndex { get => modelIndex; set => modelIndex = value; }
    public void Buy()
    {
        Debug.Log(name);
        Button prvBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        prvBtn.onClick.RemoveAllListeners();
        prvBtn.GetComponentsInChildren<Image>()[1].sprite = TestDB.instance.iconSet.GetIcon("Default");
        Equip();
    }

    public abstract void Equip();
}