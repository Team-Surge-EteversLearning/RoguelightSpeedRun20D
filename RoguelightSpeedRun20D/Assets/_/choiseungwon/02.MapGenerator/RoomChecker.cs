using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private bool isClear;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            DungeonManager.ToggleDoor(isClear);
            // 문 닫힘
            // 현재 노드 변경 Dongeon.CurrentNode

            DungeonManager.Instance.Dungeon.Current = DungeonManager.Instance.GameObjectNode[transform.parent.gameObject];
            Debug.Log(DungeonManager.Instance.Dungeon.Current.Position);
        }
    }
}
