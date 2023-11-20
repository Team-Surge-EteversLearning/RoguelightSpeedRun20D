using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathState : State
{
    private MonsterSM stateManager;

    public MonsterDeathState()
    {
        stateName = "Death";
        cooltime = -1f;
    }

    public override void Initialize(GameObject managerObject)
    {
        stateManager = managerObject.GetComponent<MonsterSM>();
    }

    protected override string StateEnter_()
    {
        stateManager.animator.Play(stateName, 0);
        stateManager.animator.Play(stateName, 1);

        cooltime = stateManager.basicData.deadTime;

        if(QuestSystem.currentQuests != null)
            foreach (Quest quest in QuestSystem.currentQuests)
                if (quest.Key == stateManager.basicData.name)
                    ((HuntingQuest)quest).UpdateCurrentCount(1);

        return "";
    }

    public override string StateUpdate()
    {
        cooltime -= Time.deltaTime;
        if(cooltime < 0)
        {
            stateManager.gameObject.SetActive(false);
        }

        return "";
    }

    protected override void StateEnd()
    {
    }

    public override void Interrupt(GameObject managerObject)
    {

    }
}