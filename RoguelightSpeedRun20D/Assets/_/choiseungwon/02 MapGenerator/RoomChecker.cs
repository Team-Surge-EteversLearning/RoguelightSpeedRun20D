using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    public Dungeon dungeon;
    
    private bool isClear;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            DungeonManager.ToggleDoor(isClear);
            // 문 닫힘
            // 현재 노드 변경 Dongeon.CurrentNode
            // 
            
        }
    }
}
