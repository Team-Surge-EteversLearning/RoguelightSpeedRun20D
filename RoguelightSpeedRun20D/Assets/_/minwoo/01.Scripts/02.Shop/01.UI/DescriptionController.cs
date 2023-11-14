using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    private float distanceAboveMouse; // ���� ����� �Ÿ�
    [SerializeField] float baseDistanceAboveMouse = 1.0f; // �⺻ �Ÿ�
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
        UpdateDistanceAboveMouse(); // �ػ󵵿� ���� �Ÿ� ������Ʈ
    }

    void UpdateDescriptionText(string descriptiontText, List<EquipmentOption> opts)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // ���콺 ���ܿ� ��ġ�� �Ÿ� ���
        Vector3 mousePosition = Input.mousePosition + new Vector3(distanceAboveMouse, distanceAboveMouse, 0);

        // ���콺 ������ ��ġ�� UI�� ���� �ϴ��� ��ġ�ϵ��� ����
        Vector2 pivot = new Vector2(0, 0);
        rectTransform.pivot = pivot;

        // UI ��ǥ�迡�� ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, mousePosition, Camera.main, out Vector3 worldPosition);

        // RectTransform ��ġ ����
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

        // ���콺 ���ܿ� ��ġ�� �Ÿ� ���
        Vector3 mousePosition = Input.mousePosition + new Vector3(distanceAboveMouse, distanceAboveMouse, 0);

        // ���콺 ������ ��ġ�� UI�� ���� �ϴ��� ��ġ�ϵ��� ����
        Vector2 pivot = new Vector2(0, 0);
        rectTransform.pivot = pivot;

        // UI ��ǥ�迡�� ���� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, mousePosition, Camera.main, out Vector3 worldPosition);

        // RectTransform ��ġ ����
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
        // ȭ�� �ػ󵵿� ���� �����ϸ� ��� ���
        float scalingFactor = Screen.height / basicRatio; // 1080p�� �������� ��

        // �Ÿ� ������Ʈ
        distanceAboveMouse = baseDistanceAboveMouse * scalingFactor;
    }
}
