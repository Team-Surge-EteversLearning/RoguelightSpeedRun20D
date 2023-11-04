using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New RoomData", menuName = "Room Data",order = 0)]
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

    public DungeonBundle dungeonBundle;
}