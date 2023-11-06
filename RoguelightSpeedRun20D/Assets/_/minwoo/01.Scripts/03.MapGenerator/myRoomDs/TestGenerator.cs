using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
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

    public GameObject indicator;
    public int roomCount = 10;
    private Dungeon _dungeon = new Dungeon();

    private DungeonNode currentNode;

    private float roomSize = 20;
    [SerializeField] float roomDistance = 30;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Generate();
        StartCoroutine(GenerateCubes());
        currentNode = _dungeon.Start;
    }
    public void MoveNode(DoorDir dir, GameObject player)
    {
        switch (dir)
        {
            case DoorDir.Front:
                if (currentNode.Front == null)
                {
                    print("막다른 길입니다");
                    break;
                }
                currentNode = currentNode.Front;
                break;
            case DoorDir.Back:
                if (currentNode.Back == null)
                {
                    print("막다른 길입니다");
                    break;
                }
                currentNode = currentNode.Back;
                break;
            case DoorDir.Right:
                if (currentNode.Right == null)
                {
                    print("막다른 길입니다");
                    break;
                }
                currentNode = currentNode.Right;
                break;
            case DoorDir.Left:
                if (currentNode.Left == null)
                {
                    print("막다른 길입니다");
                    break;
                }
                currentNode = currentNode.Left;
                break;
            default:
                print("이동명령이 아닙니다");
                break;

        }
        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = new Vector3(currentNode.Position.x * roomDistance, 0, currentNode.Position.y * roomDistance);
        controller.enabled = true;
    }
    private void Generate()
    {
        for (int i = 0; i < roomCount; i++)
        {
            _dungeon.Add();
        }
        _dungeon.SetShopNode();
    }

    private IEnumerator GenerateCubes()
    {
        GameObject room;
        foreach (var node in _dungeon)
        {
            Vector3 posi = new Vector3(node.Position.x * roomDistance, 0, node.Position.y * roomDistance);
            if (node == _dungeon.Start)
            {
                room = Instantiate(startRoomPrefab, posi, Quaternion.identity);
            }
            else if (node == _dungeon.End)
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
            DoorGenerate(node, room.transform);
            room.name = node.Position.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void DoorGenerate(DungeonNode node, Transform room)
    {
        Vector3 zPosi = room.position + new Vector3(1, 0, roomSize / 2);
        Vector3 zNegPosi = room.position - new Vector3(-1, 0, roomSize / 2);
        Vector3 xPosi = room.position + new Vector3(roomSize / 2, 0, 0);
        Vector3 xNegPosi = room.position - new Vector3(roomSize / 2, 0, 0);
        GameObject instance;
        if (node.Front != null)
        {
            instance = Instantiate(doorPrefab, zPosi, zRot);
            instance.name = "front_d";
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Front;
        }
        else
        {
            instance = Instantiate(closedWall, zPosi, zRot);
            instance.name = "front_w";
            instance.transform.position += new Vector3(0, 2, 0);

        }
        if (node.Back != null)
        {
            instance = Instantiate(doorPrefab, zNegPosi, zRot);
            instance.name = "back_d";
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Back;

        }
        else
        {
            instance = Instantiate(closedWall, zNegPosi, zRot);
            instance.name = "back_w";
            instance.transform.position += new Vector3(0, 2, 0);

        }
        if (node.Right != null)
        {
            instance = Instantiate(doorPrefab, xPosi, xRot);
            instance.name = "right_d";
            instance.transform.localPosition += new Vector3(0, 0, -0.9F);
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Right;


        }
        else
        {
            instance = Instantiate(closedWall, xPosi, xRot);
            instance.transform.position += new Vector3(0,2,0);
            instance.name = "right_w";
        }
        if (node.Left != null)
        {
            instance = Instantiate(doorPrefab, xNegPosi, xRot);
            instance.name = "Left_d";
            instance.transform.localPosition += new Vector3(0, 0, -0.9F);
            instance.GetComponentInChildren<MyDoor>().nextDoor = DoorDir.Left;


        }
        else
        {
            instance = Instantiate(closedWall, xNegPosi, xRot);
            instance.name = "Left_w";
            instance.transform.position += new Vector3(0, 2, 0);

        }

    }
}
