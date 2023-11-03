using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StairRoomData
{
    [SerializeField] private GameObject roomPrefab;

    private Vector3Int movableRoomSquare;
    private List<Vector3Int> refiningRoom;
}
