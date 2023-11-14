using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentOption
{
    public int priceOffset;
    public int sellWhenClear;
    public string optName;

    public abstract Equipment MakeEquipment(Equipment equipment);
    public abstract void UseOption();
    public abstract GameObject MakeInGame(GameObject gameObject);
}
