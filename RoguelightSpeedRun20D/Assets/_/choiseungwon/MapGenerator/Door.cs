using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorDir nextDoor;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        DungeonManager.OnDoorToggle += OnToggleDoor;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Open");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Close");
        }

    }
    
    public void OnToggleDoor(bool clear)
    {
        if (clear)
            animator.Play("OpenAnime");
        else
            animator.Play("CloseAnime");
    }
}