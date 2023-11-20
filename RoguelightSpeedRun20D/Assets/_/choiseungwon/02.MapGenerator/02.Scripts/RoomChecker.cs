using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private bool isRoomClear = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            RoomOptimizer.RoomCheck();
            DungeonManager.Instance.ChangeNode(DungeonManager.Instance.GameObjectNode[transform.parent.gameObject], transform.parent.gameObject);

            if (DungeonManager.Instance.Dungeon.Starts.Contains(DungeonManager.Instance.GameObjectNode[transform.parent.gameObject])
                || DungeonManager.Instance.GameObjectNode[transform.parent.gameObject].IsShop)
            {
                DungeonManager.Door(isRoomClear);
            }
            else
            {
                if(!DungeonManager.Instance.Dungeon.Current.isSafe)
                {
                    DungeonManager.Door(!isRoomClear);
                    if (QuestSystem.currentQuests != null)
                        foreach (Quest quest in QuestSystem.currentQuests)
                            if (quest.Key == "room")
                                ((VisitedRoomQuest)quest).UpdateCurrentCount(1);
                }
            }
        }
    }
}
