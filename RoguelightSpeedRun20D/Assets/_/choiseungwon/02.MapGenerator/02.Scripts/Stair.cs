using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public static Vector3 clearPos;

    private void Awake()
    {
        DungeonManager.ClearEvent += StairSpawn;
        DungeonManager.ClearEvent += UpStair;
    }

    private void Start()
    {
        clearPos = this.transform.position + new Vector3(0, 5, 0);
    }

    public void StairSpawn(bool clear)
    {
        if (clear == false)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (clear)
        {
            if (DungeonManager.Instance.Dungeon.Ends.Contains(DungeonManager.Instance.Dungeon.Current)
                || DungeonManager.Instance.Dungeon.Starts.Contains(DungeonManager.Instance.Dungeon.Current))
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void UpStair(bool clear)
    {
        if (clear == false)
        {
        }
        else if (clear)
        {
        }
    }
}
