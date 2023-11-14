using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorDir nextDoor;
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        DungeonManager.OnDoorToggle += ToggleDoor;
    }

    private void Start()
    {
        animator.Play("Open");
    }


    public void ToggleDoor(bool clear)
    {
        Debug.Log(clear);
        if (clear == false)
        {
            animator.Play("Close");
        }
        else if(clear)
        {
            animator.Play("Open");
        }
    }
}