using System;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorDir nextDoor;
    private Animator animator;
    [SerializeField] private bool isBossDead;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        DungeonManager.OnDoorToggle += ToggleDoor;
    }

    private void Start()
    {
        animator.Play("Open");
    }

    private void Update()
    {
        if (isBossDead)
        {
            animator.Play("Open");
        }
    }

    public void ToggleDoor(bool clear)
    {
        if (clear == false)
        {
            animator.Play("Close");
        }
    }
}