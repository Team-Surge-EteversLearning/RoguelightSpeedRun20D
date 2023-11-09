using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NormalRoomData
{
    [SerializeField] public GameObject roomPrefab;
    
    [SerializeField] private List<Vector3> boxLocationList = new List<Vector3>();
    [SerializeField] private List<Vector3> monsterLocationList = new List<Vector3>();
    [SerializeField] private List<Vector3> trapLocationList = new List<Vector3>();
    [SerializeField] private List<string> monsterSpawnPool = new List<string>();
    [SerializeField] private List<string> trapSpawnPool = new List<string>();
}
