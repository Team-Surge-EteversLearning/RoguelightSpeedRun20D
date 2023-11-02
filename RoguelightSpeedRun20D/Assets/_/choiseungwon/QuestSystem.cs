using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class QuestSystem : MonoBehaviour
{
    private List<Quest> allQuests;
    public List<Quest> currentQuests;

    void Start()
    {
        InitializeQuestsAllQuests();
    }

    private void InitializeQuestsAllQuests()
    {
        allQuests = new List<Quest>()
        {
            new Quest("사냥", "스켈레톤 3마리를 죽여라.", 100),
            new Quest("사냥", "스켈레톤 5마리를 죽여라.", 200),
            new Quest("사냥", "스켈레톤 10마리를 죽여라.", 300),
            new Quest("상자 열기", "상자 1개를 열어라.", 100),
            new Quest("상자 열기", "상자 2개를 열어라", 150),
            new Quest("상자 열기", "상자 3개를 열어라.", 200),
            new Quest("정복", "보스를 죽여라.", 500)
        };
    }

    private void SelectRandomQuests()
    {
        currentQuests = new List<Quest>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, allQuests.Count);
            currentQuests.Add(allQuests[randomIndex]);
            allQuests.RemoveAt(randomIndex);
        }
    }

    public void ResetQuests()
    {
        InitializeQuestsAllQuests();
        SelectRandomQuests();
    }
}