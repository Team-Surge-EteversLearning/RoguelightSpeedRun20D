using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxQuest : Quest
{
    public int currentCount;
    public int targetCount;
    
    public OpenBoxQuest(string key, string name, string description, int reward, int targetCount) : base(QuestType.OpenBox)
    {
        this.Key = key;
        this.Name = name;
        this.Description = description;
        this.targetCount = targetCount;
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
