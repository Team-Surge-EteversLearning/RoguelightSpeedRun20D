using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StairRoomData
{
    [SerializeField] private GameObject roomPrefab;

    [SerializeField] private Vector3Int movableRoomSquare;
    [SerializeField] private List<Vector3Int> refiningRoom;
}
