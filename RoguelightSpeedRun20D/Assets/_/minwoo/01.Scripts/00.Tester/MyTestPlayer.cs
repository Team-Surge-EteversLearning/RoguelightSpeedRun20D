using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTestPlayer : MonoBehaviour
{
    public int testFloor = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            TestGenerator.Instance.MoveNode(DoorDir.Front, gameObject);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            TestGenerator.Instance.MoveNode(DoorDir.Back, gameObject);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            TestGenerator.Instance.MoveNode(DoorDir.Right, gameObject);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            TestGenerator.Instance.MoveNode(DoorDir.Left, gameObject);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testFloor++;
            TestGenerator.Instance.MoveNode(testFloor, gameObject);

        }
    }
}
