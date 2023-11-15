//��ü �ڵ�
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DungeonNode
{
    public DungeonNode Left;
    public DungeonNode Right;
    public DungeonNode Front;
    public DungeonNode Back;
    public bool IsShop { get; set; }
    public int DistanceFromStart;
    public Vector3Int Position { get; set; }

    public int Floor { get; set; }

    public DungeonNode(int distance)
    {
        DistanceFromStart = distance;
    }
}

public class Dungeon : IEnumerable<DungeonNode>
{
    public DungeonNode Prev { get; set; }
    private DungeonNode _current;
    public DungeonNode Current
    {
        get => _current;
        set
        {
            Prev = _current;
            _current = value;
        }
    }
    public DungeonNode Start;
    private List<DungeonNode> nodes = new List<DungeonNode>();
    public DungeonNode End { get; private set; }
    public List<DungeonNode> Ends = new List<DungeonNode>();

    public List<DungeonNode> Starts = new List<DungeonNode>();
    private HashSet<KeyValuePair<DungeonNode, DungeonNode>> paths = new HashSet<KeyValuePair<DungeonNode, DungeonNode>>();
    public HashSet<KeyValuePair<DungeonNode, DungeonNode>> Paths { get => paths; set => paths = value; }
    public bool IsAlreadyHaveDoor(DungeonNode key, DungeonNode value)
    {
        if (this.Paths.Contains(new KeyValuePair<DungeonNode, DungeonNode>(key, value)) || this.Paths.Contains(new KeyValuePair<DungeonNode, DungeonNode>(value, key)))
        {
            return true;
        }
        else
            return false;
    }


    public int floor = 0;

    public Dungeon()
    {
        Start = new DungeonNode(0);
        nodes.Add(Start);
        Starts.Add(Start);
    }


    public void Add(int blockPerFloor)
    {
        if (nodes.Count % blockPerFloor == 0)
        {
            floor++;
            var newNode = new DungeonNode(0);
            newNode.Position = new Vector3Int(End.Position.x, End.Position.y + 1, End.Position.z);
            Ends.Add(End);
            Starts.Add(newNode);
            nodes.Add(newNode);
            Start = newNode;
            End = null;
            return;
        }

        var node = new DungeonNode(0);  // Initialize with 0 DistanceFromStart
        nodes.Add(node);

        if (nodes.Count == 1)
        {
            node.Position = Vector3Int.zero;
            node.DistanceFromStart = 0;
            Starts.Add(node);
        }
        else
        {
            var random = new Random();
            var availableNodes = nodes.Where(n => (n.Left == null || n.Right == null || n.Front == null || n.Back == null) && n.Position.y == floor).ToList();
            DungeonNode selectedNode = null;

            // Keep trying until we find a node with an available direction
            while (availableNodes.Count > 0)
            {
                selectedNode = availableNodes[random.Next(availableNodes.Count)];

                // Define all possible directions
                var directions = new List<Tuple<Action, Func<Vector3Int>>>
{
                    new Tuple<Action, Func<Vector3Int>>(() => {
                        if (selectedNode.Left == null && selectedNode.Position.y == floor && selectedNode != node)
                            selectedNode.Left = node;
                    }, () => selectedNode.Position + Vector3Int.left),

                    new Tuple<Action, Func<Vector3Int>>(() => {
                        if (selectedNode.Right == null && selectedNode.Position.y == floor && selectedNode != node)
                            selectedNode.Right = node;
                    }, () => selectedNode.Position + Vector3Int.right),

                    new Tuple<Action, Func<Vector3Int>>(() => {
                        if (selectedNode.Front == null && selectedNode.Position.y == floor && selectedNode != node)
                            selectedNode.Front = node;
                    }, () => selectedNode.Position + new Vector3Int(0, 0, 1)),

                    new Tuple<Action, Func<Vector3Int>>(() => {
                        if (selectedNode.Back == null && selectedNode.Position.y == floor && selectedNode != node)
                            selectedNode.Back = node;
                    }, () => selectedNode.Position + new Vector3Int(0, 0, -1))
                };

                // Shuffle the list
                directions = directions.OrderBy(x => random.Next()).ToList();
                // Try to add the node in a random direction
                foreach (var direction in directions)
                {
                    var newPosition = direction.Item2();
                    if (!nodes.Any(n => n.Position == newPosition))
                    {
                        direction.Item1();
                        node.Position = newPosition;
                        node.DistanceFromStart = selectedNode.DistanceFromStart + 1;  // Update DistanceFromStart based on the selectedNode's DistanceFromStart

                        // Check the surrounding nodes
                        var surroundingPositions = new List<Tuple<Action, Vector3Int>>
                    {
                        new Tuple<Action, Vector3Int>(() => {
                            if (node.Left == null)
                                node.Left = nodes.FirstOrDefault(n => n.Position == newPosition + Vector3Int.left && n.Position.y == floor);
                        }, newPosition + Vector3Int.left),
                        new Tuple<Action, Vector3Int>(() => {
                            if (node.Right == null)
                                node.Right = nodes.FirstOrDefault(n => n.Position == newPosition + Vector3Int.right && n.Position.y == floor);
                        }, newPosition + Vector3Int.right),
                        new Tuple<Action, Vector3Int>(() => {
                            if (node.Front == null)
                                node.Front = nodes.FirstOrDefault(n => n.Position == newPosition + Vector3Int.forward && n.Position.y == floor);
                        }, newPosition + new Vector3Int(0, 0, 1)),
                        new Tuple<Action, Vector3Int>(() => {
                            if (node.Back == null)
                                node.Back = nodes.FirstOrDefault(n => n.Position == newPosition + Vector3Int.back && n.Position.y == floor);
                        }, newPosition + new Vector3Int(0, 0, -1))
                    };

                        foreach (var pos in surroundingPositions)
                        {
                            if (nodes.Any(n => n.Position == pos.Item2 && n.Position.y == floor))
                            {
                                pos.Item1();
                                // Add reciprocal references
                                var otherNode = nodes.First(n => n.Position == pos.Item2 && n.Position.y == floor);
                                if (node.Left == otherNode) otherNode.Right = node;
                                else if (node.Right == otherNode) otherNode.Left = node;
                                else if (node.Front == otherNode) otherNode.Back = node;
                                else if (node.Back == otherNode) otherNode.Front = node;
                            }
                        }
                        break;
                    }
                }
                if (node.Position != Vector3Int.zero)
                {
                    break;
                }
                else
                {
                    availableNodes.Remove(selectedNode); // Remove the selected node from availableNodes if it's not possible to add a node in any direction.
                }
            }
        }
        // Set the End node to the one with the maximum DistanceFromStart, and the most recently added if there are multiple
        End = nodes.Where(n => n.Position.y == floor)
            .OrderByDescending(n => n.DistanceFromStart)
            .FirstOrDefault();
        Current = Starts[0];
    }




    public void AddUntil(int amount, int blockPerFloor)
    {
        for (int i = 0; i < amount; i++)
        {
            this.Add(blockPerFloor);
        }
        //Current = Starts[0];
    }


    public void SetShopNode(int amountInFloor)
    {
        var random = new Random();
        for (int i = 0; i < floor; i++)
        {
            var availableNodes = nodes.Where(n => n != Start && n != End && !Starts.Contains(n) && !Ends.Contains(n) && n.Position.y == i).ToList();

            if (availableNodes.Count > 0)
            {
                for(int j = 0; j < amountInFloor; j++)
                {
                    if (availableNodes.Count < 1)
                        break;
                    var shopNode = availableNodes[random.Next(availableNodes.Count)];
                    availableNodes.Remove(shopNode);
                    shopNode.IsShop = true;
                }
            }
            else
            {
                Debug.Log("������ ������ �� �ִ� ��尡 �����ϴ�.");
            }
        }
    }

    public IEnumerator<DungeonNode> GetEnumerator()
    {
        return nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count
    {
        get => nodes.Count;
    }


    public void Clear()
    {
        nodes.Clear();
        Start = new DungeonNode(0);
        nodes.Add(Start);
    }
}