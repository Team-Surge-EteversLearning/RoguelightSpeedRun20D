using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "New RoomData", menuName = "Room Data",order = int.MinValue)]
public class DungeonBundleData : ScriptableObject
{
    public List<StartRoomData> startRoomDatas;
    public List<NormalRoomData> normalRoomDatas;
    public List<ShopRoomData> shopRoomPresets;
    public List<BossRoomData> bossRoomPresets;
    
    public List<GameObject> doorPresets;

    public Vector3 bundleScale;
    public Vector3 dungeonPresetScale;

    public int maxRoomNum;

    public DungeonBundle dungeonBundle;
}