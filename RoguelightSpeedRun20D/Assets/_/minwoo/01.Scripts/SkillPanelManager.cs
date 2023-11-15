using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelManager : MonoBehaviour
{
    [SerializeField] GameObject skillPanel;
    [SerializeField] GameObject choicePanel;
    [SerializeField] GameObject indicator1;
    [SerializeField] GameObject indicator2;

    public static List<Button> emptySlots = new List<Button>();
    public static Dictionary<Button, ActiveSkill> btnSkillPair = new Dictionary<Button, ActiveSkill>();
    public static Action<ActiveSkill> onLearnSkill;

    private void Start()
    {
        Button[] buttons = skillPanel.GetComponentsInChildren<Button>(true);
        foreach(var item in buttons)
        {
            emptySlots.Add(item);
        }
        Debug.Log(emptySlots.Count);
        InitUnlockSkill();
        onLearnSkill = LearnSkill;
        SetIndicator();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            skillPanel.SetActive(!skillPanel.activeInHierarchy);
            SetIndicator();
        }
    }

    private void InitUnlockSkill()
    {
        //ReadDB and LearnSkill Until UnlcokSkillsCount
    }

    public void LearnSkill(ActiveSkill skill)
    {
        btnSkillPair.Add(emptySlots[0], skill);
        emptySlots[0].onClick.AddListener(() => OpenChoicePanel(skill.Name));
        emptySlots[0].GetComponentsInChildren<Image>(true)[1].sprite = TestDB.instance.iconSet.GetIcon(skill.Name);
        emptySlots.RemoveAt(0);
    }
    private void OpenChoicePanel(string name)
    {
        choicePanel.SetActive(true);
        Button[] buttons = choicePanel.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => { PlayerSM.skill1Index = name; buttons[0].onClick.RemoveAllListeners(); SetIndicator(); choicePanel.SetActive(false); });
        buttons[1].onClick.AddListener(() => { PlayerSM.skill2Index = name; buttons[1].onClick.RemoveAllListeners(); SetIndicator(); choicePanel.SetActive(false); });
    }
    private void SetIndicator()
    {
        if(PlayerSM.skill1Index != null && SkillDataModel.UnlockActive.ContainsKey(PlayerSM.skill1Index))
        {
            Debug.LogWarning(1);
            indicator1.SetActive(true);
            Button targetButton = btnSkillPair.FirstOrDefault(x => x.Value == SkillDataModel.UnlockActive[PlayerSM.skill1Index]).Key;
            RectTransform indicator1RectTransform = indicator1.GetComponent<RectTransform>();
            RectTransform targetButtonRectTransform = targetButton.GetComponent<RectTransform>();
            indicator1RectTransform.position = targetButtonRectTransform.position;
        }
        else
        {
            indicator1.SetActive(false);
        }
        if (PlayerSM.skill2Index != null && SkillDataModel.UnlockActive.ContainsKey(PlayerSM.skill2Index))
        {
            indicator2.SetActive(true);
            Button targetButton = btnSkillPair.FirstOrDefault(x => x.Value == SkillDataModel.UnlockActive[PlayerSM.skill2Index]).Key;
            RectTransform indicator2RectTransform = indicator2.GetComponent<RectTransform>();
            RectTransform targetButtonRectTransform = targetButton.GetComponent<RectTransform>();
            indicator2RectTransform.position = targetButtonRectTransform.position;
        }
        else
        {
            indicator2.SetActive(false);
        }
    }
}
