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
    public GameObject cubePrefab;
    public GameObject bossRoomPrefab;
    public GameObject startRoomPrefab;
    public GameObject shopRoomPrefab;
    public GameObject doorPrefab;
    public GameObject closedWall;
    public int roomCount = 10;
    private Dungeon _dungeon = new Dungeon();
    private DungeonNode currentNode;
<<<<<<< Updated upstream
    [SerializeField] private float testFloornob = 10;
=======
<<<<<<< HEAD
    [SerializeField]private float testFloornob = 10;
=======
    [SerializeField] private float testFloornob = 10;
>>>>>>> 96d6361a9db9f0f41c96f2f614c8a94fbc017ff5
>>>>>>> Stashed changes

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
    }

    private void DoorGenerate(DungeonNode node, Transform room)
    {
        Vector3 zPosi = room.position + new Vector3(1, 0, roomSize / 2);
        Vector3 zNegPosi = room.position - new Vector3(-1, 0, roomSize / 2);
        Vector3 xPosi = room.position + new Vector3(roomSize / 2, 0, 0);
        Vector3 xNegPosi = room.position - new Vector3(roomSize / 2, 0, 0);
        GameObject instance;
        if (node.Front != null && ! _dungeon.IsAlreadyHaveDoor(node, node.Front)) // path and door
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
}
