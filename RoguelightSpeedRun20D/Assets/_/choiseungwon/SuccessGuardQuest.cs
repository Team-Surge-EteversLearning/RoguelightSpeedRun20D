using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessGuardQuest : Quest
{
    public int currentCount;
    public int targetCount;
    
    public SuccessGuardQuest(string name, string description, int targetCount, int rewardGold) : base(QuestType.SuccessGuard)
    {
        this.Name = name;
        this.Description = description;
        this.targetCount = targetCount;
        this.rewardGold = rewardGold;
    }

    public void UpdateCurrentCount(int count)
    {
        this.currentCount += count;
        CheckQuestStatus();
    }
    
    public override void CheckQuestStatus()
    {
        if (currentCount >= targetCount)
        {
            IsCompleted = true;
        }
    }
}
