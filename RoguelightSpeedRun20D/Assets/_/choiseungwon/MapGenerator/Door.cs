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

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         animator.Play("Open");
    //     }
    //
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         animator.Play("Close");
    //
    //     }
    // }

    public void ToggleDoor(bool clear)
    {
        if (clear)
        {
            animator.Play("Open");
        }
        else
        {
            animator.Play("Close");
        }
    }
}