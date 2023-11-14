using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBoxQuest : Quest
{
    public int currentCount;
    public int targetCount;
    
    public OpenBoxQuest(string name, string description, int reward, int targetCount) : base(QuestType.OpenBox)
    {
        this.Name = name;
        this.Description = description;
        this.targetCount = targetCount;
    }

    public override void CheckQuestStatus()
    {
        throw new System.NotImplementedException();
    }
}
