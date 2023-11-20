using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private Vector3 afterPosition;
    private Vector3 beforePosition;
    private void Awake()
    {
        DungeonManager.SpawnStairEvent += SpawnStair;
        DungeonManager.BossRoomClearEvent += UpStair;
    }

    private void Start()
    {
        beforePosition = transform.GetChild(1).transform.position;
        afterPosition = transform.GetChild(2).transform.position;
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
        if (clear == true)
        {
            StartCoroutine(UpStairCoroutine());
        }
    }

    IEnumerator UpStairCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);

            this.transform.position = Vector3.MoveTowards(this.transform.position, afterPosition, 0.5f);
        }
        
        yield return new WaitForSeconds(10f);

        this.transform.position = Vector3.MoveTowards(this.transform.position, beforePosition, 4.75f);
    }
}
