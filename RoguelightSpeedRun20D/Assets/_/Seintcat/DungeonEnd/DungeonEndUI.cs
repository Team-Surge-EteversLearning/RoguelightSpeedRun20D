using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DungeonEnd(bool gameClear)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        singleton.gameObject.SetActive(true);
        singleton.title.text = (gameClear ? "Dungeon Clear" : "Game Over");
        singleton.nextbutton.SetActive(gameClear);
    }

    public void NextStage()
    {

    }

    public void GoVillage()
    {

    }
}
