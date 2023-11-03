using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBundleData : ScriptableObject
{
    public List<NormalRoomData> flatRoomPresets;
    public List<ShopRoomData> shopRoomPresets;
    public List<BossRoomData> bossRoomPresets;
    public List<StairRoomData> stairRoomPresets;
    
    public List<GameObject> doorPresets;

    public Vector3Int bundleScale;
    public Vector3Int dungeonPresetScale;

    public int maxRoomNum;

    private DungeonBundle dungeonBundle;
}