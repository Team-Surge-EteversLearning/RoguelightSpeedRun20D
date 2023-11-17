using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private void Awake()
    {
        DungeonManager.SpawnStairEvent += SpawnStair;
        DungeonManager.BossRoomClearEvent += UpStair;
    }

    public void SpawnStair(bool clear)
    {
        if (clear == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
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
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                this.transform.position + new Vector3(0, 0.5f, 0), 1);
            yield return new WaitForSeconds(0.7f);
        }
        yield return new WaitForSeconds(10f);
        
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, -5f, 0), 5);
    }
}
