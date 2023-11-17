using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCharacterPanelController : MonoBehaviour
{
    [SerializeField] TMP_Text damageTxt;
    [SerializeField] TMP_Text attackSpeedTxt;
    [SerializeField] TMP_Text hpTxt;
    [SerializeField] TMP_Text staminaTxt;
    [SerializeField] TMP_Text mpTxt;
    [SerializeField] TMP_Text powerTxt;
    [SerializeField] TMP_Text speedTxt;

    [SerializeField] Image headImg;
    [SerializeField] Image torsoImg;
    [SerializeField] Image shoesImg;
    //public static Dictionary<string, TMP_Text> nameTxtPair = new Dictionary<string, TMP_Text>();
    //public static Action<string> onStatChange;
    //private void Awake()
    //{
    //    PlayerSM
    //    onStatChange = StatViewChange;
    //}
    //private void StatViewChange(string stat)
    //{

    //}
    public void ResetUI()
    {
        damageTxt.text = $"DAMAGE : {PlayerSM.weaponNow.Damage * PlayerSM.powerWeight}";
        attackSpeedTxt.text = $"ATTACKSPEED : {PlayerSM.weaponNow.Cooltime}";
        hpTxt.text = $"HP : {PlayerSM.hpNow}/{PlayerSM.hpMax}";
        staminaTxt.text = $"STAMINA : {(int)PlayerSM.staminaNow}/{PlayerSM.staminaMax}";
        mpTxt.text = $"MP : {PlayerSM.manaNow}/{PlayerSM.manaMax}";
        powerTxt.text = $"Power : {PlayerSM.powerWeight}";
        speedTxt.text = $"SPEED : {PlayerSM.moveSpeed}";

        headImg.sprite = TestDB.instance.iconSet.GetIcon(PlayerSM.weaponNow.Name);
        torsoImg.sprite = TestDB.instance.iconSet.GetIcon(PlayerSM.armorNow.Name);
        shoesImg.sprite = TestDB.instance.iconSet.GetIcon(PlayerSM.shoesNow.Name);
    }
}
