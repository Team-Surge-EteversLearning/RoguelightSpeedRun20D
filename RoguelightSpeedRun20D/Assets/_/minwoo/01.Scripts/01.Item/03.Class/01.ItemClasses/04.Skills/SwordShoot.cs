﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordShoot : ActiveSkill
{
    public SwordShoot(string name, int mana, float coolTime) : base(name, mana, coolTime)
    {
        SkillDecription += " : Not Gilgamesh";
    }

    protected override void Init()
    {
        base.Init();
    }
        
    public override void _Use()
    {
        PlayerSM playerSM = PlayerSM.playerObj.GetComponent<PlayerSM>();
        GameObject projectile = playerSM.skillPrefabs[3];
        //SetRotation
        Vector3 direction = playerSM.skillTransform.forward; 
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject instantiatedObject = UnityEngine.Object.Instantiate(projectile, playerSM.skillTransform.position, rotation);
        instantiatedObject.GetComponent<AttackAble>().AttackStart();
    }
}

