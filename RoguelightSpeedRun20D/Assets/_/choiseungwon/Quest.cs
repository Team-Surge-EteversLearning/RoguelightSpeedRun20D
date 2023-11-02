using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string name;
    public string explanation;
    public int reward;
    
    public bool isCompleted;

    public Quest(string name, string explanation, int reward)
    {
        this.name = name;
        this.explanation = this.explanation;
        this.reward = reward;
        this.isCompleted = false;
    }

    public void CompleteQuest()
    {
        this.isCompleted = true;
    }

    public void FailQuest()
    {
        this.isCompleted = false;
    }
}