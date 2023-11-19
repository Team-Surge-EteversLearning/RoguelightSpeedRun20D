using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEndUI : MonoBehaviour
{
    private static DungeonEndUI singleton;

    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private GameObject nextbutton;

    private void Awake()
    {
        singleton = this;
        gameObject.SetActive(false);
        DungeonManager.BundleClearEvent += DungeonEnd;
    }
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void DungeonEnd(bool gameClear)
    {
        Debug.LogWarning("DungeonEnd");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        singleton.gameObject.SetActive(true);
        singleton.title.text = (gameClear ? "Dungeon Clear" : "Game Over");
        singleton.nextbutton.SetActive(gameClear);
    }

    public void NextStage()
    {
        // SceneManager.LoadScene()
    }

    public void GoVillage()
    {
        SceneManager.LoadScene(0);

        PlayerStatsManager.CashNow += PlayerSM.weaponNow.SellWhenClear;
        PlayerStatsManager.CashNow += PlayerSM.armorNow.SellWhenClear;
        PlayerStatsManager.CashNow += PlayerSM.shoesNow.SellWhenClear;

        PlayerSaveManager.SaveData("default", PlayerStatsManager.WareHouseCash, 
            PlayerStatsManager.HpMax, PlayerStatsManager.StaminaMax, PlayerStatsManager.ManaMax,
            PlayerStatsManager.PowerWeight, PlayerSM.skill1Index,
            PlayerSM.skill2Index, PlayerSaveManager.WrappingUnlocks());
    }
}
