using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public static Vector3 clearPos;

    private void Awake()
    {
        DungeonManager.BossRoomClearEvent += StairSpawn;
        DungeonManager.BossRoomClearEvent += UpStair;
    }

    private void Start()
    {
        clearPos = this.transform.position + new Vector3(0, 5, 0);
    }

    private void Update()
    {
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
        StartCoroutine(UpStair());
    }

    IEnumerator UpStair()
    {
        for (int i = 0; i < 10; i++)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, 0.5f, 0), 1);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
