using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private void Awake()
    {
        DungeonManager.ClearEvent += ToggleStair;
    }

    public void ToggleStair(bool clear)
    {
        if (clear == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else if(clear)
        {
            if (DungeonManager.Instance.Dungeon.Ends.Contains(DungeonManager.Instance.Dungeon.Current))
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
