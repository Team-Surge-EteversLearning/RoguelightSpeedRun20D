using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private Image questImage;
    [SerializeField] private Text[] questTitle;
    [SerializeField] private Text[] questDescription;
    [SerializeField] private Text[] questReward;
    
    private List<Quest> allQuests;
    public List<Quest> currentQuests;

    void Start()
    {
        InitializeQuestsAllQuests();
        SelectRandomQuests();
    }

    private void InitializeQuestsAllQuests()
    {
        allQuests = new List<Quest>()
        {
            new HuntingQuest("스켈레톤 사냥", "스켈레톤을 3마리 잡으세요.", 3, 100),
            new HuntingQuest("스켈레톤 사냥", "스켈레톤을 5마리 잡으세요.", 5, 200),
            new HuntingQuest("스켈레톤 사냥", "스켈레톤을 7마리 잡으세요.", 7, 300),
            new HuntingQuest("스켈레톤 사냥", "스켈레톤을 10마리 잡으세요.", 10, 500),
            
            new ColletItemQuest("루비 수집가", "루비를 3개 획득하세요.", 3, 100),
            new ColletItemQuest("루비 수집가", "루비를 5개 획득하세요.", 5, 200),
            new ColletItemQuest("루비 수집가", "루비를 7개 획득하세요.", 7, 300),
            new ColletItemQuest("루비 수집가", "루비를 10개 획득하세요.", 10, 500),
            
            new OpenBoxQuest("상자 사랑꾼", "상자를 1개 여세요.", 1, 100),
            new OpenBoxQuest("상자 사랑꾼", "상자를 2개 여세요.", 2, 200),
            new OpenBoxQuest("상자 사랑꾼", "상자를 3개 여세요.", 3, 300),
            new OpenBoxQuest("상자 사랑꾼", "상자를 4개 여세요.", 5, 500),
            
            new TotalDamageQuest("약한 싸움꾼", "총 데미지 1000 달성", 1000, 100),
            new TotalDamageQuest("조금 약한 싸움꾼", "총 데미지 2000 달성", 2000, 250),
            new TotalDamageQuest("노련한 싸움꾼", "총 데미지 3000 달성", 3000, 500),
            new TotalDamageQuest("진정한 싸움꾼", "총 데미지 5000 달성", 5000, 1000),
            
            new VisitedRoomQuest("귀차니즘", "10개의 방 방문", 10, 100),
            new VisitedRoomQuest("노련한 탐험가", "15개의 방 방문", 15, 250),
            new VisitedRoomQuest("진정한 탐험가", "20개의 방 방문", 20, 500),
            new VisitedRoomQuest("뭐하는 놈이지?", "30개의 방 방문", 30, 1000),
            
            new ClearTimeQuest("어리바리 까지마라 ㅋㅋ", "30분 안에 클리어", 10, 100),
            new ClearTimeQuest("일반인", "20분 안에 클리어", 20, 250),
            new ClearTimeQuest("평범한 한국인", "15분 안에 클리어", 15, 500),
            new ClearTimeQuest("진정한 한국인", "10분 안에 클리어", 10, 1000),
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(currentQuests[0]);
            Debug.Log(currentQuests[1]);
            Debug.Log(currentQuests[2]);
        }
    }

    public void ResetQuests()
    {
        InitializeQuestsAllQuests();
        SelectRandomQuests();
    }

    public void OpenQuest()
    {
        questImage.gameObject.SetActive(true);
    }
    
    public void CloseQuest()
    {
        questImage.gameObject.SetActive(false);
    }
}