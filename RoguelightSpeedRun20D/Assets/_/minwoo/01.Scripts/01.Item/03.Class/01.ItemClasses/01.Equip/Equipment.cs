
using UnityEngine;
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
    }
}