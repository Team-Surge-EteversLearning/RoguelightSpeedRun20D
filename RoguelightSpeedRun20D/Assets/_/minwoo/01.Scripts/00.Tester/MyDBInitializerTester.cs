using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDBInitializerTester : MonoBehaviour
{
   // [ContextMenu("Init")]
    private void Start()
    {
        EquipmentDataManager eqdb = new EquipmentDataManager();
        eqdb.Init();
        DungeonItemManager dungeon = new DungeonItemManager();
        dungeon.InitPrcieTable();
        PlayerStatsManager playerstats = new PlayerStatsManager();
        playerstats.Init();
        SkillDataModel skillDataModel = new SkillDataModel();
        skillDataModel.Init();

        PlayerSaveManager.LoadData("default");

    }
}
