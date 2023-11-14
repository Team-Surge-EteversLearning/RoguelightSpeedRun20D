using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    private float distanceAboveMouse; // 실제 사용할 거리
    [SerializeField] float baseDistanceAboveMouse = 1.0f; // 기본 거리
    [SerializeField] float basicRatio = 1080f;
    [SerializeField] TMP_Text descriptionTxt;
    [SerializeField] TMP_Text optList;

    public static Action<string, List<EquipmentOption>> onDescriptionWithOpts;
    public static Action<string> onDescription;
    public static Action onDescriptionComplete;


    void Start()
    {
        onDescriptionWithOpts = UpdateDescriptionText;
        onDescription = UpdateDescriptionText;
        onDescriptionComplete = endDescription;
        gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateDistanceAboveMouse(); // 해상도에 따른 거리 업데이트
    }

    void UpdateDescriptionText(string descriptiontText, List<EquipmentOption> opts)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 마우스 우상단에 위치할 거리 계산
        Vector3 mousePosition = Input.mousePosition + new Vector3(distanceAboveMouse, distanceAboveMouse, 0);

        // 마우스 포인터 위치에 UI의 왼쪽 하단이 위치하도록 보정
        Vector2 pivot = new Vector2(0, 0);
        rectTransform.pivot = pivot;

        // UI 좌표계에서 월드 좌표로 변환
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, mousePosition, Camera.main, out Vector3 worldPosition);

        // RectTransform 위치 설정
        rectTransform.position = worldPosition;
        gameObject.SetActive(true);
        descriptionTxt.text = descriptiontText;
        foreach (var item in opts)
        {
            optList.text += $"{item.optName}\n";
        }
    }
    void UpdateDescriptionText(string descriptiontText)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 마우스 우상단에 위치할 거리 계산
        Vector3 mousePosition = Input.mousePosition + new Vector3(distanceAboveMouse, distanceAboveMouse, 0);

        // 마우스 포인터 위치에 UI의 왼쪽 하단이 위치하도록 보정
        Vector2 pivot = new Vector2(0, 0);
        rectTransform.pivot = pivot;

        // UI 좌표계에서 월드 좌표로 변환
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, mousePosition, Camera.main, out Vector3 worldPosition);

        // RectTransform 위치 설정
        rectTransform.position = worldPosition;
        gameObject.SetActive(true);
        descriptionTxt.text = descriptiontText;
    }
    void endDescription()
    {
        gameObject.SetActive(false);
        descriptionTxt.text = "";
        optList.text = "";
    }

    void UpdateDistanceAboveMouse()
    {
        // 화면 해상도에 따른 스케일링 요소 계산
        float scalingFactor = Screen.height / basicRatio; // 1080p를 기준으로 함

        // 거리 업데이트
        distanceAboveMouse = baseDistanceAboveMouse * scalingFactor;
    }
}
