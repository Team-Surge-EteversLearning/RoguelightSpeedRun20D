using System;
using UnityEngine;
public enum DoorDirection
{
    Front,
    Back, Left, Right
}
public class Door : MonoBehaviour
{
    public DoorDir nextDoor;
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        TestGenerator.OnDoorToggle += ToggleDoor;
    }


    public void ToggleDoor(bool flag)
    {
        if (flag)
            animator.SetTrigger("Open");
        else
            animator.SetTrigger("Close");
    }
}