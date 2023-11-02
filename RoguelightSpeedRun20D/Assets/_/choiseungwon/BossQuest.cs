using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : Quest
{
    public BossQuest(QuestType type) : base(QuestType.KillBoss)
    {
    }

    public override void CheckQuestStatus()
    {
        throw new System.NotImplementedException();
    }
}
