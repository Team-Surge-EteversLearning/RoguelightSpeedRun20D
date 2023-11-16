using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomQuest : Quest
{
    public BossRoomQuest(QuestType type) : base(type)
    {
    }

    protected override void _CheckQuestStatus()
    {
        throw new System.NotImplementedException();
    }

    public override string GetProgress()
    {
        throw new System.NotImplementedException();
    }
}
