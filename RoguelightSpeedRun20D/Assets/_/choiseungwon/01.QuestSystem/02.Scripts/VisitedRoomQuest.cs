using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitedRoomQuest : Quest
{
    private int currentCount;
    private int targetCount;
    
    public VisitedRoomQuest(string name, string description, int targetCount, int rewardGold) : base(QuestType.VisitedRoom)
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
