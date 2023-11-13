using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public List<Door> currentRoomDoors;

    public bool isMonsterSpawnRoom;

    public List<Vector3> boxSpawnPoint;
    public Dictionary<Vector3, string> monsterSpawnPoint;
    public Dictionary<Vector3, string> trapSpawnPoint;
}
