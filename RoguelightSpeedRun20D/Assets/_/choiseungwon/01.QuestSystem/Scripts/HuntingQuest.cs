using System.Diagnostics;

public class HuntingQuest : Quest
{
    private int currentCount;
    private int targetCount;

    public HuntingQuest(string key, string name, string description, int targetCount, int rewardGold) : base(QuestType.Hunting)
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
            PlayerStatsManager.CashNow += rewardGold;
            UnityEngine.Debug.LogWarning("!!!!!!EEE@E");
            DungeonShopManager.onBuy("");
        }
    }
    public override string GetProgress()
    {
        return "(" + currentCount + " / " + targetCount + ")";
    }
}