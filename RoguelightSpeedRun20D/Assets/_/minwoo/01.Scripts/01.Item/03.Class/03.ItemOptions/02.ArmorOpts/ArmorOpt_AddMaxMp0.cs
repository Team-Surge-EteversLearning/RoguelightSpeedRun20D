using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorOpt_AddMaxMp0 : EquipmentOption
{
    public ArmorOpt_AddMaxMp0(int priceOffset) : base(priceOffset)
    {
    }

    public override Equipment MakeEquipment(Equipment equipment)
	{
		this.optName = "Int_0";
		Armor tempArmor = (Armor)equipment;
		tempArmor.MaxMana += 10;
		tempArmor.ManaRegen += 1;
        tempArmor.usableOptions.Add(this);
		return tempArmor;
	}

	public override GameObject MakeInGame(GameObject gameObject)
	{
        HeadOutFits currentHandler = gameObject.GetComponent<HeadOutFits>(); // material

		return gameObject;
	}

	public override void UseOption()
	{
		return;
	}
}
