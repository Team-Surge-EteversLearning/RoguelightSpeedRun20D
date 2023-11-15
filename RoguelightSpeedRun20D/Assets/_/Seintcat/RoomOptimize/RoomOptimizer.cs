using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOptimizer : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> renderers;

    private static List<RoomOptimizer> rooms = new List<RoomOptimizer>();

    private static readonly float distanceCheckValue = 50f;

    private void Awake()
    {
        rooms.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void RoomCheck() 
    {
        Vector3 objPos;
        if (PlayerSM.playerObj == null)
            objPos = Camera.main.transform.position;
        else
            objPos = PlayerSM.playerObj.transform.position;

        foreach (RoomOptimizer roomOptimizer in rooms)
        {
            if(Vector3.Distance(roomOptimizer.transform.position, objPos) < distanceCheckValue)
            {
                foreach (MeshRenderer renderer in roomOptimizer.renderers)
                    renderer.enabled = true;
            }
            else
            {
                foreach (MeshRenderer renderer in roomOptimizer.renderers)
                    renderer.enabled = false;
            }
        }
    }

    public static void ResetRoomList()
    {
        rooms.Clear();
    }
}
