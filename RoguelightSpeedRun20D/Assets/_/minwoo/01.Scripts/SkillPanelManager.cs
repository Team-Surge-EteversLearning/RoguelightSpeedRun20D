using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPanelManager : MonoBehaviour
{
    [SerializeField] GameObject skillPanel;
    [SerializeField] GameObject choicePanel;
    [SerializeField] GameObject indicator1;
    [SerializeField] GameObject indicator2;
    [SerializeField] GameObject skillNotificationPanel;

    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject optPanel;
    [SerializeField] GameObject outfit;

    public static List<Button> emptySlots = new List<Button>();
    public static Dictionary<Button, ActiveSkill> btnSkillPair = new Dictionary<Button, ActiveSkill>();
    public static Action<ActiveSkill> onLearnSkill;

    private void Start()
    {
        Button[] buttons = skillPanel.GetComponentsInChildren<Button>(true);
        foreach (var item in buttons)
        {
            emptySlots.Add(item);
        }
        //Debug.Log(emptySlots.Count);
        InitUnlockSkill();
        onLearnSkill = LearnSkill;
        SetIndicator();
    }

    public void OpenPanel()
    {
        skillPanel.SetActive(!skillPanel.activeInHierarchy);
        optPanel.SetActive(!skillPanel.activeInHierarchy);
        shopPanel.SetActive(!optPanel.activeInHierarchy);
        outfit.SetActive(!skillPanel.activeInHierarchy);
        SetIndicator();
    }

    private void InitUnlockSkill()
    {
        //ReadDB and LearnSkill Until UnlcokSkillsCount
    }

    public void LearnSkill(ActiveSkill skill)
    {
        if (btnSkillPair.Any(pair => pair.Value == skill))
            return;

        btnSkillPair.Add(emptySlots[0], skill);
        emptySlots[0].onClick.AddListener(() => OpenChoicePanel(skill.Name));
        emptySlots[0].GetComponentsInChildren<Image>(true)[1].sprite = TestDB.instance.iconSet.GetIcon(skill.Name);

        EventTrigger eventTrigger = emptySlots[0].gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerEnterEntry.callback.AddListener((eventData) => { ShopUI.OnPointEnterProduct(skill.SkillDecription); });
        eventTrigger.triggers.Add(pointerEnterEntry);

        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((eventData) => { OnPointExitProduct(); });
        eventTrigger.triggers.Add(pointerExitEntry);

        emptySlots.RemoveAt(0);
    }
    private void OpenChoicePanel(string name)
    {
        choicePanel.SetActive(true);
        Button[] buttons = choicePanel.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => { PlayerSM.skill1Index = name; buttons[0].onClick.RemoveAllListeners(); SetIndicator(); choicePanel.SetActive(false); });
        buttons[1].onClick.AddListener(() => { PlayerSM.skill2Index = name; buttons[1].onClick.RemoveAllListeners(); SetIndicator(); choicePanel.SetActive(false); });

        Debug.LogWarning($"{PlayerSM.skill1Index} / {PlayerSM.skill2Index}");
    }
    private void SetIndicator()
    {
        if (PlayerSM.skill1Index != null && SkillDataModel.UnlockActive.ContainsKey(PlayerSM.skill1Index))
        {
            Debug.LogWarning(1);
            indicator1.SetActive(true);
            Button targetButton = btnSkillPair.FirstOrDefault(x => x.Value == SkillDataModel.UnlockActive[PlayerSM.skill1Index]).Key;
            indicator1.transform.parent = targetButton.transform;
            RectTransform indicator1RectTransform = indicator1.GetComponent<RectTransform>();
            indicator1RectTransform.anchoredPosition = Vector3.zero;
        }
        else
        {
            indicator1.SetActive(false);
        }
        if (PlayerSM.skill2Index != null && SkillDataModel.UnlockActive.ContainsKey(PlayerSM.skill2Index))
        {
            indicator2.SetActive(true);
            Button targetButton = btnSkillPair.FirstOrDefault(x => x.Value == SkillDataModel.UnlockActive[PlayerSM.skill2Index]).Key;
            indicator2.transform.parent = targetButton.transform;
            RectTransform indicator2RectTransform = indicator2.GetComponent<RectTransform>();
            indicator2RectTransform.anchoredPosition = Vector3.zero;
        }
        else
        {
            indicator2.SetActive(false);
        }
    }
    private void OnPointExitProduct()
    {
        DescriptionController.onDescriptionComplete?.Invoke();
    }
}
