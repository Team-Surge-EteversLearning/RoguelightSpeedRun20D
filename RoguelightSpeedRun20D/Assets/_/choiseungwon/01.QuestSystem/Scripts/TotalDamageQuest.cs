using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalDamageQuest : Quest
{
    public float currentDamage;
    public float targetDamage;

    public TotalDamageQuest(string name, string description, float targetDamage, int rewardGold) : base(QuestType.TotalDamage)
    {
        this.Key = "damage";
        this.Name = name;
        this.Description = description;
        this.targetDamage = targetDamage;
        this.rewardGold = rewardGold;
    }
    
    public void UpdateCurrentCount(float count)
    {
        this.currentDamage += count;
        CheckQuestStatus();
    }

    public override void CheckQuestStatus()
    {
        if (currentDamage >= targetDamage)
        {
            IsCompleted = true;
        }
    }
}
