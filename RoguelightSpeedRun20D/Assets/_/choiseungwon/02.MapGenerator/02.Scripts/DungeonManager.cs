using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance { get; set; }

    [SerializeField] private List<DungeonBundleData> dungeonBundleDatas = new List<DungeonBundleData>();
    [SerializeField] private MiniMapManager miniMapManager;

    public Dictionary<GameObject, DungeonNode> GameObjectNode = new Dictionary<GameObject, DungeonNode>();

    private int roomCount;
    private int roomInFloor;
    private float floorHeight;
    private float roomDistance;

    private int normalRoomrandNum;
    private int shopRoomrandNum;

    private Dungeon _dungeon = new Dungeon();
    public Dungeon Dungeon
    {
        get => _dungeon;
        set => _dungeon = value;
    }
    Dictionary<DungeonNode, Transform> roomNodeTransformPair = new Dictionary<DungeonNode, Transform>();
    
    public delegate void DoorToggleDelegate(bool clear);
    public static event DoorToggleDelegate OnDoorToggle;
    
    [SerializeField] private int _currentMonsterCount;
    public int CurrentMonsterCount 
    {
        get => _currentMonsterCount; 
        set 
        {
            _currentMonsterCount = value;
            if(_currentMonsterCount == 0)
            {
                Dungeon.Current.isSafe = true;
                ToggleDoor(true);
            }
        } 
    }

    public static void ToggleDoor(bool isClear)
    {
        OnDoorToggle?.Invoke(isClear);
    }
    
    private void Awake()
    {
        Instance = this;
        
        roomCount = dungeonBundleDatas[0].roomCount;
        roomInFloor = dungeonBundleDatas[0].roomInFloor;
        floorHeight = dungeonBundleDatas[0].floorHeight;
        roomDistance = dungeonBundleDatas[0].roomDistance;
        
        GameObjectNode.Clear();
    }

    void Start()
    {
        RoomOptimizer.ResetRoomList();
        Generate(_dungeon);
        
        GenerateRoom(_dungeon, floorHeight);
        miniMapManager.MiniMapCreate();
        floorHeight += 10;
    }
    public void ChangeNode(DungeonNode node, GameObject go)
    {
        _dungeon.Current = node;
        Debug.LogWarning(Dungeon.Current.isSafe);
        miniMapManager.ChangeMinimapNode(Dungeon.Current, Dungeon.Prev);
        CurrentMonsterCount = go.GetComponentsInChildren<MonsterSM>(true).Length;
        foreach(var item in go.GetComponentsInChildren<MonsterSM>(true))
        {
            item.gameObject.SetActive(true);
        }
    }
    
    private void Generate(Dungeon target)
    {
        target.AddUntil(roomCount, roomInFloor);
        target.SetShopNode(3);
    }

    private void GenerateRoom(Dungeon target, float height)
    {
        GameObject room;
        foreach (var node in target)
        {
            Debug.Log(target.Ends.Count);

            Vector3 posi = new Vector3(node.Position.x * roomDistance, height * node.Position.y, node.Position.z * roomDistance);
            
            if (node == target.Starts[0])
            {
                room = Instantiate(dungeonBundleDatas[0].startRoomPresets[0].roomPrefab, posi, Quaternion.identity);
            }
            else if (target.Starts.Contains(node))
            {
                room = Instantiate(dungeonBundleDatas[0].startRoomPresets[1].roomPrefab, posi, Quaternion.identity);
            }
            else if (target.Ends.Contains(node))
            {
                if (node == target.Ends[target.Ends.Count - 1])
                {
                    room = Instantiate(dungeonBundleDatas[0].bossRoomPresets[1].roomPrefab, posi, Quaternion.identity);
                    continue;
                }
                room = Instantiate(dungeonBundleDatas[0].bossRoomPresets[0].roomPrefab, posi, Quaternion.identity); // Stair point
                Instantiate(dungeonBundleDatas[0].stair, posi, Quaternion.identity);
            }
            else if (node.IsShop)
            {
                room = Instantiate(dungeonBundleDatas[0].shopRoomPresets[shopRoomrandNum].roomPrefab, posi, Quaternion.identity);
            }
            else
            {
                room = Instantiate(dungeonBundleDatas[0].normalRoomPresets[normalRoomrandNum].roomPrefab, posi, Quaternion.identity);
            }
            roomNodeTransformPair.Add(node, room.transform);
            DoorGenerate(node, room.transform);
            room.name = node.Position.ToString();
            GameObjectNode.Add(room, node);
            normalRoomrandNum = Random.Range(0, 39);
            shopRoomrandNum = Random.Range(0, 10);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
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
        if (node.Front != null && !_dungeon.IsAlreadyHaveDoor(node, node.Front)) // path and door
        {
            instance = Instantiate(dungeonBundleDatas[0].doorPresets, frontDoorPosi, frontRot);
            instance.name = node.Front.Position.ToString();
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Front;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Front));
        }
        else if (node.Front == null)
        {
            instance = Instantiate(dungeonBundleDatas[0].wall, frontWallPos, frontRot);
            instance.name = "front_w";
        }
        
        if (node.Right != null && !_dungeon.IsAlreadyHaveDoor(node, node.Right))
        {
            instance = Instantiate(dungeonBundleDatas[0].doorPresets, rightDoorPosi, rightRot);
            instance.name = node.Right.Position.ToString();
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Right;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Right));
        
        }
        else if (node.Right == null)
        {
            instance = Instantiate(dungeonBundleDatas[0].wall, rightWallPos, rightRot);
            instance.name = "right_w";
        }
        
        if (node.Back != null && !_dungeon.IsAlreadyHaveDoor(node, node.Back))
        {
            instance = Instantiate(dungeonBundleDatas[0].doorPresets, backDoorPosi, backRot);
            instance.name = node.Back.Position.ToString();
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Back;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Back));
        }
        else if (node.Back == null)
        {
            instance = Instantiate(dungeonBundleDatas[0].wall, backWallPos, backRot);
            instance.name = "back_w";
        }
        
        if (node.Left != null && !_dungeon.IsAlreadyHaveDoor(node, node.Left))
        {
            instance = Instantiate(dungeonBundleDatas[0].doorPresets, leftDoorPosi, leftRot);
            instance.name = node.Left.Position.ToString();
            instance.GetComponentInChildren<Door>().nextDoor = DoorDir.Left;
            _dungeon.Paths.Add(new KeyValuePair<DungeonNode, DungeonNode>(node, node.Left));
        }
        else if (node.Left == null)
        {
            instance = Instantiate(dungeonBundleDatas[0].wall, leftWallPos, leftRot);
            instance.name = "Left_w";
        }
    }
}