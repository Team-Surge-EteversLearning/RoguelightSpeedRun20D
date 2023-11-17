using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "New RoomData", menuName = "Room Data",order = int.MinValue)]
public class DungeonBundleData : ScriptableObject
{
    public static DungeonBundleData dungeonBundleData;
    
    public List<StartRoomData> startRoomPresets;
    public List<NormalRoomData> normalRoomPresets;
    public List<ShopRoomData> shopRoomPresets;
    public List<BossRoomData> bossRoomPresets;
    
    public GameObject doorPresets;
    public GameObject wall;
    public GameObject stair;
    // public GameObject stairPosition;
    
    public int roomCount = 10;
    public int roomInFloor;
    public float floorHeight = 10;
    public float roomDistance = 30;
}