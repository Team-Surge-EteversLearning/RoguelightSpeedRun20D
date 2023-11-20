using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorOpt_Special1 : EquipmentOption
{
    public ArmorOpt_Special1(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
	{
		this.optName = "Specail_1";
		Armor tempArmor = (Armor)equipment;
		tempArmor.MaxHp += 7;
		tempArmor.MaxMana += 7;
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
