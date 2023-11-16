using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessGuardQuest : Quest
{
    public int currentCount;
    public int targetCount;
    
    public SuccessGuardQuest(string description, int targetCount, int rewardGold) : base(QuestType.SuccessGuard)
    {
        this.Name = "Guard";
        this.Description = description;
        this.targetCount = targetCount;
        this.rewardGold = rewardGold;
    }

    public void UpdateCurrentCount(int count)
    {
        this.currentCount += count;
        CheckQuestStatus();
    }
    
    protected override void _CheckQuestStatus()
    {
        if (currentCount >= targetCount)
        {
            IsCompleted = true;
        }
    }

    public override string GetProgress()
    {
        return "(" + currentCount + " / " + targetCount + ")";
    }
}
