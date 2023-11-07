using System;
using UnityEngine;
public enum DoorDir
{
    Front,
    Back, Left, Right
}
public class MyDoor : MonoBehaviour
{
    public DoorDir nextDoor;
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        TestGenerator.OnDoorToggle += ToggleDoor;
    }


    public void ToggleDoor(bool flag)
    {
        if (flag)
            animator.SetTrigger("Open");
        else
            animator.SetTrigger("Close");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TestGenerator.Instance.MoveNode(nextDoor, other.gameObject);
        }
    }
}