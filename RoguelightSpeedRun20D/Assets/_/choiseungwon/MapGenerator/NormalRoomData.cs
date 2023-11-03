using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NormalRoomData
{
    [SerializeField] private GameObject roomPrefab;

    [SerializeField] private bool front;
    [SerializeField] private bool back;
    [SerializeField] private bool right;
    [SerializeField] private bool left;

    private List<Vector3> boxLocationList = new List<Vector3>();
    private List<Vector3> monsterLocationList = new List<Vector3>();
    private List<Vector3> trapLocationList = new List<Vector3>();
    private List<string> monsterSpawnPool = new List<string>();
    private List<string> trapSpawnPool = new List<string>();
}
