using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using TMPro;
using UnityEditor.ShaderGraph.Internal;

public class QuestSystem : MonoBehaviour
{
    private List<Quest> allQuests;
    public static List<Quest> currentQuests;
    private static QuestSystem singleton;

    private float time;

    [SerializeField] private List<TextMeshProUGUI> titles;
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private List<TextMeshProUGUI> progress;
    [SerializeField] private List<TextMeshProUGUI> rewards;

    private void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        ResetQuests();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (currentQuests[0] is ClearTimeQuest || currentQuests[1] is ClearTimeQuest || currentQuests[2] is ClearTimeQuest)
        {
            foreach (ClearTimeQuest quest in currentQuests)
            {
                quest.UpdateCurrentTime(Time.deltaTime);

                if (quest.IsCompleted == true)
                {
                    Debug.Log("ClearTimeQuestClear");
                }
                else
                {
                    Debug.Log("ClearTimeQuestFail");
                }
            }
        }
    }


    private void InitializeQuestsAllQuests()
    {
        allQuests = new List<Quest>()
        {
            new HuntingQuest("Skeleton", "스켈레톤 사냥", "스켈레톤 세 마리 사냥.", 3, 100),
            new HuntingQuest("Skeleton", "스켈레톤 사냥", "스켈레톤 다섯 마리 사냥", 5, 200),
            new HuntingQuest("Skeleton", "스켈레톤 사냥", "스켈레톤 일곱 마리 사냥", 7, 300),
            new HuntingQuest("Skeleton", "스켈레톤 사냥", "스켈레톤 열 마리 사냥", 10, 500),
            
            new HuntingQuest("Crab", "게 사냥", "게 한 마리 사냥", 1, 100),
            new HuntingQuest("Crab", "게 사냥", "게 두 마리 사냥", 2, 200),
            new HuntingQuest("Crab", "게 사냥", "게 세 마리 사냥", 3, 500),
            new HuntingQuest("Crab", "게 집게로 그냥", "게 다섯 마리 사냥", 5, 1000),
            
            new HuntingQuest("Minotaur", "미노타우르스 사냥", "미노타우르스 한 마리 사냥", 1, 100),
            new HuntingQuest("Minotaur", "미노타우르스 사냥", "미노타우르스 두 마리 사냥", 2, 200),
            new HuntingQuest("Minotaur", "미노타우르스 사냥", "미노타우르스 세 마리 사냥", 3, 500),
            new HuntingQuest("Minotaur", "미노타우르스 사냥", "미노타우르스 다섯 마리 사냥", 5, 1000),
            
            new HuntingQuest("pumpkin", "호박 사냥", "호박 한 마리 사냥", 1, 100),
            new HuntingQuest("pumpkin", "호박 사냥", "호박 두 마리 사냥", 2, 200),
            new HuntingQuest("pumpkin", "호박 사냥", "호박 세 마리 사냥", 3, 500),
            new HuntingQuest("pumpkin", "Happy Halloween!", "호박 다섯 마리 사냥", 5, 10000),
            
            new HuntingQuest("Ghost", "유령이다!", "유령 한 마리 사냥", 1, 100),
            new HuntingQuest("Ghost", "유령이다!", "유령 두 마리 사냥", 2, 200),
            new HuntingQuest("Ghost", "유령이다!", "유령 세 마리 사냥", 3, 500),
            new HuntingQuest("Ghost", "유령이다!", "유령 다섯 마리 사냥", 5, 1000),
            
            new HuntingQuest("Wolf", "개잡이", "개 한 마리 사냥", 1, 100),
            new HuntingQuest("Wolf", "개잡이", "개 두 마리 사냥", 2, 200),
            new HuntingQuest("Wolf", "개잡이", "개 다섯 마리 사냥", 5, 500),
            new HuntingQuest("Wolf", "진짜 개잡이", "개 열 마리 사냥", 3, 1000),
            
            new HuntingQuest("Zombie", "좀비 사냥꾼", "좀비 한 마리 사냥", 1, 100),
            new HuntingQuest("Zombie", "좀비 사냥꾼", "좀비 세 마리 사냥", 3, 200),
            new HuntingQuest("Zombie", "좀비 사냥꾼", "좀비 다섯 마리 사냥", 5, 500),
            new HuntingQuest("Zombie", "좀비 사냥꾼", "좀비 열 마리 사냥", 10, 1000),

             new CollectItemQuest("longSword", "수집가", "Long Sword 수집", 1, 100),
             new CollectItemQuest("armingSword", "수집가", "ArmingSword 수집", 1, 100),
             new CollectItemQuest("cutlass", "수집가", "Cutlass 수집", 1, 200),
             new CollectItemQuest("rapier", "수집가", "rapier 수집", 1, 100),
             new CollectItemQuest("superSledgehammer", "수집가", "Super Sledge Hammer 수집", 1, 200),
             new CollectItemQuest("warhammer", "수집가", "War Hammer 수집", 1, 200),
             new CollectItemQuest("ironHelm", "수집가", "Iron Helm 수집", 1, 200),
             new CollectItemQuest("steelHelm", "수집가", "Steel Helm 수집", 1, 200),

            new OpenBoxQuest("상자 사랑꾼", "상자를 1개 여세요.", 1, 100),
            new OpenBoxQuest("상자 사랑꾼", "상자를 2개 여세요.", 2, 200),
            new OpenBoxQuest("상자 사랑꾼", "상자를 3개 여세요.", 3, 300),
            new OpenBoxQuest("상자 사랑꾼", "상자를 4개 여세요.", 5, 500),
            
            new VisitedRoomQuest("뭐하노", "10개의 몬스터 방 방문", 10, 100),
            new VisitedRoomQuest("노련한 탐험가", "15개의 몬스터 방 방문", 15, 250),
            new VisitedRoomQuest("진정한 탐험가", "20개의 몬스터 방 방문", 20, 500),
            new VisitedRoomQuest("미친 자", "30개의 몬스터 방 방문", 30, 1000),
            //
            // new ClearTimeQuest("여유로운 자", "30분 안에 클리어", 1800, 100),
            // new ClearTimeQuest("일반인", "20분 안에 클리어", 1200, 250),
            // new ClearTimeQuest("평범한 한국인", "15분 안에 클리어", 900, 500),
            // new ClearTimeQuest("진정한 한국인", "10분 안에 클리어", 50, 1000),
            //
            new SuccessGuardQuest("방어 성공 1회","방어 성공 1회시, 완료", 1, 100),
            new SuccessGuardQuest("방어 성공 2회","방어 성공 2회시, 완료", 2, 200),
            new SuccessGuardQuest("방어 성공 3회","방어 성공 3회시, 완료", 3, 300),
            new SuccessGuardQuest("방어 성공 4회","방어 성공 4회시, 완료", 4, 500),
        };
    }

    private void SelectRandomQuests()
    {
        currentQuests = new List<Quest>();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, allQuests.Count);
            currentQuests.Add(allQuests[randomIndex]);
        }
    }

    public void ResetQuests()
    {
        InitializeQuestsAllQuests();
        SelectRandomQuests();
        UpdateUI();
    }

    public static void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!currentQuests[i].IsCompleted)
            {
                singleton.titles[i].text = currentQuests[i].Name;
                singleton.texts[i].text = currentQuests[i].Description;
                singleton.progress[i].text = currentQuests[i].GetProgress();
                singleton.rewards[i].text = currentQuests[i].rewardGold + "";
            }
            else
            {
                singleton.titles[i].text = "Cleared";
                singleton.texts[i].text = "Cleared";
                singleton.progress[i].text = "";
                singleton.progress[i].text = "";
            }
        }
    }
}