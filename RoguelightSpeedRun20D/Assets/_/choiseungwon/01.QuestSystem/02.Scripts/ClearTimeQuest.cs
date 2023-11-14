using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTimeQuest : Quest
{
    public float currentTime;
    public float targetTime;
    
    public ClearTimeQuest(string name, string description, float targetTime, int rewardGold) : base(QuestType.ClearTime)
    {
        this.Name = name;
        this.Description = description;
        this.targetTime = targetTime;
        this.rewardGold = rewardGold;
    }

    public void UpdateCurrentCount(float count)
    {
        this.currentTime += count;
        CheckQuestStatus();
    }
    
    public override void CheckQuestStatus()
    {
        if (currentTime <= targetTime)
        {
            IsCompleted = true;
        }
    }
}
