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

    public void UpdateCurrentTime(float time)
    {
        this.currentTime += time;
    }

    protected override void _CheckQuestStatus()
    {
        if (currentTime <= targetTime)
        {
            IsCompleted = true;
            PlayerStatsManager.CashNow += rewardGold;

        }
    }

    public override string GetProgress()
    {
        return "("+ Mathf.CeilToInt(currentTime) +" / " + targetTime + ")";
    }
}
