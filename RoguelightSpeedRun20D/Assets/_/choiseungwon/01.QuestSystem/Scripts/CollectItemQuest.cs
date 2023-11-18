using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectItemQuest : Quest
{
    private int targetCount;
    private int currentCount;

    public CollectItemQuest(string key, string name, string description, int targetCount, int rewardGold) 
        : base(QuestType.CollectItem)
    {
        this.Key = key;
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
