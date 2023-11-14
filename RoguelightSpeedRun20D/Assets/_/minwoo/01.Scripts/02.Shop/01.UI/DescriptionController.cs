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
        // ���콺 �� ���⿡ ��ġ�� �Ÿ� ���
        RectTransform rectTransform = GetComponent<RectTransform>();

        // ���콺 �� ���⿡ ��ġ�� �Ÿ� ���
        Vector3 mousePosition = Input.mousePosition + new Vector3(distanceAboveMouse, distanceAboveMouse, 0);

        // UI ��ǥ�迡�� ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, mousePosition, Camera.main, out Vector3 worldPosition);

        // RectTransform ��ġ ����
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
