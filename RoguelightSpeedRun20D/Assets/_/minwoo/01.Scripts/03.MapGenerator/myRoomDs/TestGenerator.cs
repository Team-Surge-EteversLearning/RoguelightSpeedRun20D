using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

public class TestGenerator : MonoBehaviour
{
    private Quaternion zRot = Quaternion.Euler(0f, 90, 00);
    private Quaternion xRot = Quaternion.Euler(0f, 180, 0);

    //const int xRot = 180;

    public static TestGenerator Instance { get; set; }
    public DungeonNode CurrentNode
    {
        get => currentNode;
        set
        {
            DungeonNode prevNode = currentNode;
            currentNode = value;
            ChangeMinimapNode(currentNode, prevNode);
        }
    }

    public GameObject cubePrefab;
    public GameObject bossRoomPrefab;
    public GameObject startRoomPrefab;
    public GameObject shopRoomPrefab;
    public GameObject doorPrefab;
    public GameObject closedWall;
    public int roomCount = 10;
    private Dungeon _dungeon = new Dungeon();
    private DungeonNode currentNode;

    [SerializeField] private float testFloornob = 10;
    private float roomSize = 20;
    [SerializeField] float roomDistance = 30;


    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject startIndicator;
    [SerializeField] int roomInFloor;
    [SerializeField] GameObject miniMapCube;
    Dictionary<DungeonNode, Transform> roomNodeTransformPair = new Dictionary<DungeonNode, Transform>();
    private Dictionary<DungeonNode, GameObject> miniMapDict = new Dictionary<DungeonNode, GameObject>();

    public delegate void DoorToggleDelegate(bool flag);
    public static event DoorToggleDelegate OnDoorToggle;

    public bool testFlag;
    public int testFloor = 0;
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

        StartCoroutine(GenerateRooms(_dungeon, testFloornob));
    }


    private void Update()
    {

    }
    public void MoveNode(int floor, GameObject player)
    {
        CurrentNode = _dungeon.Starts[floor];
        player.transform.position = roomNodeTransformPair[_dungeon.Starts[floor]].position;
        ChangeFloor(floor);
    }

    public void MoveNode(DoorDir dir, GameObject player)
    {
        switch (dir)
        {
            case DoorDir.Front:
                if (CurrentNode.Front == null)
                {
                    print("���ٸ� ���Դϴ�");
                    CurrentNode = CurrentNode;
                    break;
                }
                CurrentNode = CurrentNode.Front;
                break;
            case DoorDir.Back:
                if (CurrentNode.Back == null)
                {
                    print("���ٸ� ���Դϴ�");
                    CurrentNode = CurrentNode;

                    break;
                }
                CurrentNode = CurrentNode.Back;
                break;
            case DoorDir.Right:
                if (CurrentNode.Right == null)
                {
                    print("���ٸ� ���Դϴ�");
                    CurrentNode = CurrentNode;
                    break;
                }
                CurrentNode = CurrentNode.Right;
                break;
            case DoorDir.Left:
                if (CurrentNode.Left == null)
                {
                    print("���ٸ� ���Դϴ�");
                    CurrentNode = CurrentNode;
                    break;
                }
                CurrentNode = CurrentNode.Left;
                break;
            default:
                print("�̵������ �ƴմϴ�");
                break;

        }
        player.transform.position = roomNodeTransformPair[CurrentNode].position;
    }
    private void Generate(Dungeon target)
    {
        target.AddUntil(roomCount, roomInFloor);
        target.SetShopNode();
    }

    private IEnumerator GenerateRooms(Dungeon target, float height)
    {
        GameObject room;
        foreach (var node in target)
        {
            Vector3 posi = new Vector3(node.Position.x * roomDistance, height * node.Position.y, node.Position.z * roomDistance);
            if (node == target.Start)
            {
                room = Instantiate(startRoomPrefab, posi, Quaternion.identity);
            }
            else if (node == target.End)
            {
                room = Instantiate(bossRoomPrefab, posi, Quaternion.identity);
            }
            else if (node.IsShop)
            {
                room = Instantiate(shopRoomPrefab, posi, Quaternion.identity);
            }
            else
            {
                room = Instantiate(cubePrefab, posi, Quaternion.identity);
            }
            MiniMapCreate(node);
            roomNodeTransformPair.Add(node, room.transform);
            DoorGenerate(node, room.transform);
            room.name = node.Position.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        foreach (var item in target.Ends)
        {
            GameObject instance = Instantiate(indicator, roomNodeTransformPair[item]);
        }
        foreach (var item in target.Starts)
        {
            GameObject instance = Instantiate(startIndicator, roomNodeTransformPair[item]);
        }
        CurrentNode = _dungeon.Starts[0];
    }

    private void DoorGenerate(DungeonNode node, Transform room)
    {
        Vector3 zPosi = room.position + new Vector3(1, 0, roomSize / 2);
        Vector3 zNegPosi = room.position - new Vector3(-1, 0, roomSize / 2);
        Vector3 xPosi = room.position + new Vector3(roomSize / 2, 0, 0);
        Vector3 xNegPosi = room.position - new Vector3(roomSize / 2, 0, 0);
        GameObject instance;
        if (node.Front != null && !_dungeon.IsAlreadyHaveDoor(node, node.Front)) // path and door
        {
            instance = Instantiate(doorPrefab, zPosi, zRot);
            instance.name = "front_d";
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Front;

            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Front));
        }
        else if (node.Front == null)
        {
            instance = Instantiate(closedWall, zPosi, zRot);
            instance.name = "front_w";
            instance.transform.position += new Vector3(0, 2, 0);

        }
        if (node.Back != null && !_dungeon.IsAlreadyHaveDoor(node, node.Back))
        {
            instance = Instantiate(doorPrefab, zNegPosi, zRot);
            instance.name = "back_d";
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Back;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Back));
        }
        else if (node.Back == null)
        {
            instance = Instantiate(closedWall, zNegPosi, zRot);
            instance.name = "back_w";
            instance.transform.position += new Vector3(0, 2, 0);

        }
        if (node.Right != null && !_dungeon.IsAlreadyHaveDoor(node, node.Right))
        {
            instance = Instantiate(doorPrefab, xPosi, xRot);
            instance.name = "right_d";
            instance.transform.localPosition += new Vector3(0, 0, -0.9F);
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Right;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Right));

        }
        else if (node.Right == null)
        {
            instance = Instantiate(closedWall, xPosi, xRot);
            instance.transform.position += new Vector3(0, 2, 0);
            instance.name = "right_w";
        }
        if (node.Left != null && !_dungeon.IsAlreadyHaveDoor(node, node.Left))
        {
            instance = Instantiate(doorPrefab, xNegPosi, xRot);
            instance.name = "Left_d";
            instance.transform.localPosition += new Vector3(0, 0, -0.9F);
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Left;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Left));
        }
        else if (node.Left == null)
        {
            instance = Instantiate(closedWall, xNegPosi, xRot);
            instance.name = "Left_w";
            instance.transform.position += new Vector3(0, 2, 0);
        }
    }

    private void MiniMapCreate(DungeonNode node)
    {
        GameObject miniNode = Instantiate(miniMapCube);
        miniNode.name = "mini" + node.Position.ToString();
        miniNode.transform.position = new Vector3(node.Position.x, node.Position.y, node.Position.z);
        miniMapDict.Add(node, miniNode);
        if (node.Position.y != 0)
        {
            miniNode.SetActive(false);
        }
    }

    private void ChangeMinimapNode(DungeonNode current, DungeonNode prv = null)
    {
        if (prv != null)
            miniMapDict[prv].GetComponent<SpriteRenderer>().color = Color.white;
        if (miniMapDict.ContainsKey(current))
            miniMapDict[current].GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void ChangeFloor(int next)
    {
        foreach(var item in miniMapDict)
        {
            if(item.Key.Position.y == next)
            {
                item.Value.SetActive(true);
            }
            else
            {
                item.Value.SetActive(false);
            }
        }
    }
}
