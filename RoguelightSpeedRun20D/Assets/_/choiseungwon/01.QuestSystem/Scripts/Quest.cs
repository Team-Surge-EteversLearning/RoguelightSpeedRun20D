public abstract class Quest
{
    public string Key { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
     public bool IsCompleted { get; set; }
     public QuestType QuestType { get; set;}
    
     public int rewardGold { get; set; }

    
    protected Quest(QuestType type) 
    {
        QuestType = type;
    }

    protected abstract void _CheckQuestStatus();
    public void CheckQuestStatus()
    {
        _CheckQuestStatus();
        QuestSystem.UpdateUI();
    }

    public abstract string GetProgress();
}

public enum QuestType
{
    Hunting,
    CollectItem,
    OpenBox,
    TotalDamage,
    VisitedRoom,
    ClearTime,
    SuccessGuard,
}