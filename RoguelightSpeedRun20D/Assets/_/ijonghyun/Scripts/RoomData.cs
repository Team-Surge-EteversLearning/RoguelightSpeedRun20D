using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : ScriptableObject
{
    public string roomName; 
    public GameObject roomPrefab;
    public int roomSizeX = 1; 
    public int roomSizeZ = 1;
}

