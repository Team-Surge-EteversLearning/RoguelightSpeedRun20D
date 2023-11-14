using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    public float distanceAboveMouse = 1.0f;
    [SerializeField] TMP_Text descriptionTxt;

    public static Action<string> onDescription;
    public static Action onDescriptionComplete;


    void Start()
    {
        onDescription = UpdateDescriptionText;
        onDescriptionComplete = endDescription;
        gameObject.SetActive(false);
    }

    void UpdateDescriptionText(string text)
    {
        // 마우스 윗 방향에 위치할 거리 계산
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 마우스 윗 방향에 위치할 거리 계산
        Vector3 mousePosition = Input.mousePosition + new Vector3(distanceAboveMouse, distanceAboveMouse, 0);

        // UI 좌표계에서 월드 좌표로 변환
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, mousePosition, Camera.main, out Vector3 worldPosition);

        // RectTransform 위치 설정
        rectTransform.position = worldPosition;
        gameObject.SetActive(true);
        descriptionTxt.text = text;
    }
    void endDescription()
    {
        gameObject.SetActive(false);
        descriptionTxt.text = "";
    }
}
