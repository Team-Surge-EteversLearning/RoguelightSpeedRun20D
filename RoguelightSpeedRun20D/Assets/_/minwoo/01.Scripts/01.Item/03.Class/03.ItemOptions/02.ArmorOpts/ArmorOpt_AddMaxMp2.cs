using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorOpt_AddMaxMp2 : EquipmentOption
{
    public ArmorOpt_AddMaxMp2(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
	{
		this.optName = "Int_2";
		Armor tempArmor = (Armor)equipment;
		tempArmor.MaxMana += 15;
		tempArmor.ManaRegen += 2;
        tempArmor.usableOptions.Add(this);
		return tempArmor;
	}

	public override GameObject MakeInGame(GameObject gameObject)
	{
        HeadOutFits currentHandler = gameObject.GetComponent<HeadOutFits>(); // material
		currentHandler.materialIndex = 0;

		return gameObject;
	}

	public override void UseOption()
	{
		return;
	}
}
