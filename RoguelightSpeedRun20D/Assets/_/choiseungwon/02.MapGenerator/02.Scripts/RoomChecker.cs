using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private bool isClear = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            RoomOptimizer.RoomCheck();

            if (DungeonManager.Instance.Dungeon.Starts.Contains(DungeonManager.Instance.GameObjectNode[transform.parent.gameObject])
                || DungeonManager.Instance.GameObjectNode[transform.parent.gameObject].IsShop)
            {
                DungeonManager.ToggleDoor(isClear);
            }
            else
            {
                DungeonManager.ToggleDoor(!isClear);
                if (QuestSystem.currentQuests != null)
                    foreach (Quest quest in QuestSystem.currentQuests)
                        if (quest.Key == "room")
                            ((VisitedRoomQuest)quest).UpdateCurrentCount(1);
            }

            // 문 닫힘
            // 현재 노드 변경 Dongeon.CurrentNode

            DungeonManager.Instance.ChangeNode(DungeonManager.Instance.GameObjectNode[transform.parent.gameObject], transform.parent.gameObject);
        }
    }
}
