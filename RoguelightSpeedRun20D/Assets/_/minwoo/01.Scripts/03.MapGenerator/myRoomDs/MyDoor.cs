using UnityEngine;

public enum DoorDir
{
    Front,
    Back, Left, Right
}
public class MyDoor : MonoBehaviour
{
    public DoorDir nextDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TestGenerator.Instance.MoveNode(nextDoor, other.gameObject);
        }
    }
}