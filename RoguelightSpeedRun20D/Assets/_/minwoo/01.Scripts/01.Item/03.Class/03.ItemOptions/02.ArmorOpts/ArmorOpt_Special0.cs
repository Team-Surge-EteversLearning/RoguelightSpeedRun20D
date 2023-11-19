using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorOpt_Special0 : EquipmentOption
{
    public ArmorOpt_Special0(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
	{
		this.optName = "Int_2";
		Armor tempArmor = (Armor)equipment;
		tempArmor.MaxHp += 5;
		tempArmor.MaxMana += 5;
		tempArmor.ManaRegen += 1;
        tempArmor.usableOptions.Add(this);
		return tempArmor;
	}

	public override GameObject MakeInGame(GameObject gameObject)
	{
        HeadOutFits currentHandler = gameObject.GetComponent<HeadOutFits>(); // material
		currentHandler.materialIndex = 1;

		return gameObject;
	}

	public override void UseOption()
	{
		return;
	}
}
