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

    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject optPanel;
    [SerializeField] GameObject outfit;
    [SerializeField] GameObject shopKeeper;

    [SerializeField] Button toggleBtn;

    public static List<Button> emptySlots = new List<Button>();
    public static Dictionary<Button, ActiveSkill> btnSkillPair = new Dictionary<Button, ActiveSkill>();
    public static Action<ActiveSkill> onLearnSkill;

    private void Awake()
    {
        Button[] buttons = skillPanel.GetComponentsInChildren<Button>(true);
        foreach (var item in buttons)
        {
            emptySlots.Add(item);
        }
        //Debug.Log(emptySlots.Count);
        onLearnSkill = LearnSkill;
        SetIndicator();
        toggleBtn.onClick.AddListener(OpenPanel);
    }

    public void OpenPanel()
    {
        skillPanel.SetActive(true);
        optPanel.SetActive(false);
        shopPanel.SetActive(false);
        outfit.SetActive(false);
        shopKeeper.SetActive(false);

        toggleBtn.onClick.RemoveListener(OpenPanel);
        toggleBtn.onClick.AddListener(ClosePanel);

        SetIndicator();
    }
    public void ClosePanel()
    {
        skillPanel.SetActive(false);
        optPanel.SetActive(true);
        shopPanel.SetActive(false);
        outfit.SetActive(true);
        shopKeeper?.SetActive(true);

        toggleBtn.onClick.RemoveListener(ClosePanel);
        toggleBtn.onClick.AddListener(OpenPanel);
        SetIndicator();
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
        buttons[0].onClick.AddListener(() => { PlayerSM.skill1Index = name; buttons[0].onClick.RemoveAllListeners(); SetIndicator(); choicePanel.SetActive(false); Debug.LogWarning(PlayerSM.skill1Index);
        });
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
    private void OnDestroy()
    {
        emptySlots.Clear();
    }
    private void OnPointExitProduct()
    {
        DescriptionController.onDescriptionComplete?.Invoke();
    }
}
