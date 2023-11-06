
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditorInternal.ReorderableList;

public abstract class Equipment : IProduct
{
    private string name;
    private int sellWhenClear;
    private EquipmentType type;

    public string Name { get => name; set => name = value; }
    public int SellWhenClear { get => sellWhenClear; set => sellWhenClear = value; }
    public EquipmentType Type { get => type; set => type = value; }

    public void Buy()
    {
        Debug.Log(name);
        Button prvBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        prvBtn.onClick.RemoveAllListeners();
        prvBtn.image.sprite = TestDB.instance.iconSet.GetIcon("Default");
        Equip();
    }

    public abstract void Equip();
}