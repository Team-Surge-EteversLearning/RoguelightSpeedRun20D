using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RoomType {}
public class DungeonManager : MonoBehaviour
{
    [SerializeField] private List<DungeonBundleData> dungeonBundleDatas = new List<DungeonBundleData>();

    public static DungeonManager Instance { get; set; }
    
    public GameObject door;
    public GameObject closedWall;
    public int roomCount = 10;
    
    
    private Dungeon _dungeon = new Dungeon();
    private DungeonNode currentNode;

    [SerializeField] private float testFloornob = 10;
    private float roomSize = 20;
    [SerializeField] float roomDistance = 30;

    Dictionary<DungeonNode, Transform> roomNodeTransformPair = new Dictionary<DungeonNode, Transform>();
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject startIndicator;
    [SerializeField] int roomInFloor;

    public delegate void DoorToggleDelegate(bool flag);
    public static event DoorToggleDelegate OnDoorToggle;

    public bool testFlag;
    [ContextMenu("testClear")]
    private void TestClear()
    {
        OnDoorToggle?.Invoke(testFlag);
        testFlag = !testFlag;
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Generate(_dungeon);


        StartCoroutine(GenerateCubes(_dungeon, testFloornob));
        testFloornob += 10;
        currentNode = _dungeon.Start;

    }

 
    public void MoveNode(DoorDir dir, GameObject player)
    {
        switch (dir)
        {
            case DoorDir.Front:
                if (currentNode.Front == null)
                {
                    print("���ٸ� ���Դϴ�");
                    break;
                }
                currentNode = currentNode.Front;
                break;
            case DoorDir.Back:
                if (currentNode.Back == null)
                {
                    print("���ٸ� ���Դϴ�");
                    break;
                }
                currentNode = currentNode.Back;
                break;
            case DoorDir.Right:
                if (currentNode.Right == null)
                {
                    print("���ٸ� ���Դϴ�");
                    break;
                }
                currentNode = currentNode.Right;
                break;
            case DoorDir.Left:
                if (currentNode.Left == null)
                {
                    print("���ٸ� ���Դϴ�");
                    break;
                }
                currentNode = currentNode.Left;
                break;
            default:
                print("�̵������ �ƴմϴ�");
                break;

        }
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        Debug.Log(roomNodeTransformPair[currentNode].position);
        player.transform.position = roomNodeTransformPair[currentNode].position;
        controller.enabled = true;
    }
    private void Generate(Dungeon target)
    {
        target.AddUntil(roomCount, roomInFloor);
        target.SetShopNode();
    }

    private IEnumerator GenerateCubes(Dungeon target, float height)
    {
        int randNum = Random.Range(0, 7);
        
        GameObject room;
        foreach (var node in target)
        {
            Vector3 posi = new Vector3(node.Position.x * roomDistance, height * node.Position.y, node.Position.z * roomDistance);
            if (node == target.Start)
            {
                room = Instantiate(dungeonBundleDatas[0].startRoomDatas[0].roomPrefab, posi, Quaternion.identity);
            }
            else if (node == target.End)
            {
                room = Instantiate(dungeonBundleDatas[0].bossRoomPresets[0].roomPrefab, posi, Quaternion.identity);
            }
            else if (node.IsShop)
            {
                room = Instantiate(dungeonBundleDatas[0].shopRoomPresets[0].roomPrefab, posi, Quaternion.identity);
            }
            else
            {
                room = Instantiate(dungeonBundleDatas[0].normalRoomDatas[randNum].roomPrefab, posi, Quaternion.identity);
            }
            roomNodeTransformPair.Add(node, room.transform);
            DoorGenerate(node, room.transform);
            room.name = node.Position.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        // foreach (var item in target.Ends)
        // {
        //     GameObject instance = Instantiate(indicator, roomNodeTransformPair[item]);
        // }
        // foreach (var item in target.Starts)
        // {
        //     GameObject instance = Instantiate(startIndicator, roomNodeTransformPair[item]);
        // }
    }

    private void DoorGenerate(DungeonNode node, Transform room)
    {
        Vector3 frontDoorPosi = room.position + new Vector3(0.69f, 0.15f, 10);
        Vector3 rightDoorPosi = room.position + new Vector3(10,0.15f,-0.66f);
        Vector3 backDoorPosi = room.position + new Vector3(-0.66f, 0.15f, -10);
        Vector3 leftDoorPosi = room.position + new Vector3(-10, 0.15f, 0.69f);
        
        Vector3 frontWallPos = room.position + new Vector3(0.015f, 1.2f, 10);
        Vector3 rightWallPos = room.position + new Vector3(10, 1.2f, 0.015f);
        Vector3 backWallPos = room.position + new Vector3(0.015f, 1.2f, -10);
        Vector3 leftWallPos = room.position + new Vector3(-10, 1.2f, 0.015f);
        
        Quaternion frontRot = Quaternion.Euler(0, -270, 0);
        Quaternion rightRot = Quaternion.Euler(0, 180, 0);
        Quaternion backRot = Quaternion.Euler(0, 270, 0);
        Quaternion leftRot = Quaternion.Euler(0, 0, 0);
        
        GameObject instance;
        if (node.Front != null && ! _dungeon.IsAlreadyHaveDoor(node, node.Front)) // path and door
        {
            instance = Instantiate(door, frontDoorPosi, frontRot);
            instance.name = "front_d";
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Front;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Front));
        }
        else if (node.Front == null)
        {
            instance = Instantiate(closedWall, frontWallPos, frontRot);
            instance.name = "front_w";
        }
        
        if (node.Right != null && !_dungeon.IsAlreadyHaveDoor(node, node.Right))
        {
            instance = Instantiate(door, rightDoorPosi, rightRot);
            instance.name = "right_d";
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Right;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Right));
        
        }
        else if (node.Right == null)
        {
            instance = Instantiate(closedWall, rightWallPos, rightRot);
            instance.name = "right_w";
        }
        
        if (node.Back != null && !_dungeon.IsAlreadyHaveDoor(node, node.Back))
        {
            instance = Instantiate(door, backDoorPosi, backRot);
            instance.name = "back_d";
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Back;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Back));
        }
        else if (node.Back == null)
        {
            instance = Instantiate(closedWall, backWallPos, backRot);
            instance.name = "back_w";
        }
        
        if (node.Left != null && !_dungeon.IsAlreadyHaveDoor(node, node.Left))
        {
            instance = Instantiate(door, leftDoorPosi, leftRot);
            instance.name = "Left_d";
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Left;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Left));
        }
        else if (node.Left == null)
        {
            instance = Instantiate(closedWall, leftWallPos, leftRot);
            instance.name = "Left_w";
        }
    }
}
