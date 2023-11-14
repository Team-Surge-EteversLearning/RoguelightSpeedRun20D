public class HuntingQuest : Quest
{
    private int currentCount;
    private int targetCount;

    public HuntingQuest(string name, string description, int targetCount, int rewardGold) : base(QuestType.Hunting) 
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