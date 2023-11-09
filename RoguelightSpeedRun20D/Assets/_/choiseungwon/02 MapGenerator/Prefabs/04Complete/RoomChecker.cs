using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private bool isClear;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DungeonManager.ToggleDoor(isClear);
            // 문 닫힘
            // 현재 노드 변경 Dongeon.CurrentNode
            // 
        }
    }
}
