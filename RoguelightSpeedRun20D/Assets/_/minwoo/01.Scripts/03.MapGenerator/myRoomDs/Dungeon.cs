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
    public DungeonNode Start;
    private List<DungeonNode> nodes = new List<DungeonNode>();
    public DungeonNode End { get; private set; }
    public List<DungeonNode> Ends = new List<DungeonNode>();
    public List<DungeonNode> Starts = new List<DungeonNode>(); // 스타츠 0

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
            node.Position = Vector3Int.zero;  // ù ����� ��ġ�� (0,0)���� �ʱ�ȭ
            node.DistanceFromStart = 0;
            Starts.Add(node);
        }
        else
        {
            var random = new Random();
            var availableNodes = nodes.Where(n => (n.Left == null || n.Right == null || n.Front == null || n.Back == null) && n.Position.y == floor).ToList(); DungeonNode selectedNode = null;

            // Keep trying until we find a node with an available direction
            while (availableNodes.Count > 0)
            {
                selectedNode = availableNodes[random.Next(availableNodes.Count)];

                // Define all possible directions
                var directions = new List<Tuple<Action, Func<Vector3Int>>>
            {
                new Tuple<Action, Func<Vector3Int>>(() => selectedNode.Left = node, () => selectedNode.Position + Vector3Int.left),
                new Tuple<Action, Func<Vector3Int>>(() => selectedNode.Right = node, () => selectedNode.Position + Vector3Int.right),
                new Tuple<Action, Func<Vector3Int>>(() => selectedNode.Front = node, () => selectedNode.Position + new Vector3Int(0, 0, 1)),
                new Tuple<Action, Func<Vector3Int>>(() => selectedNode.Back = node, () => selectedNode.Position + new Vector3Int(0, 0, -1))
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
                            new Tuple<Action, Vector3Int>(() => node.Left = nodes.First(n => n.Position == newPosition + Vector3Int.left), newPosition + Vector3Int.left),
                            new Tuple<Action, Vector3Int>(() => node.Right = nodes.First(n => n.Position == newPosition + Vector3Int.right), newPosition + Vector3Int.right),
                            new Tuple<Action, Vector3Int>(() => node.Front = nodes.First(n => n.Position == newPosition + Vector3Int.forward), newPosition + new Vector3Int(0, 0, 1)),
                            new Tuple<Action, Vector3Int>(() => node.Back = nodes.First(n => n.Position == newPosition + Vector3Int.back), newPosition + new Vector3Int(0, 0, -1))
                        };


                        foreach (var pos in surroundingPositions)
                        {
                            if (nodes.Any(n => n.Position == pos.Item2))
                            {
                                pos.Item1();
                                // Add reciprocal references
                                if (node.Left == nodes.First(n => n.Position == pos.Item2)) nodes.First(n => n.Position == pos.Item2).Right = node;
                                else if (node.Right == nodes.First(n => n.Position == pos.Item2)) nodes.First(n => n.Position == pos.Item2).Left = node;
                                else if (node.Front == nodes.First(n => n.Position == pos.Item2)) nodes.First(n => n.Position == pos.Item2).Back = node;
                                else if (node.Back == nodes.First(n => n.Position == pos.Item2)) nodes.First(n => n.Position == pos.Item2).Front = node;
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
        End = nodes.Where(n => n.Position.y == floor).OrderByDescending(n => n.DistanceFromStart).FirstOrDefault();
    }



    public void AddUntil(int amount, int blockPerFloor)
    {
        for (int i = 0; i < amount; i++)
        {
            this.Add(blockPerFloor);
        }
    }


    public void SetShopNode()
    {
        var random = new Random();
        var availableNodes = nodes.Where(n => n != Start && n != End).ToList();  // Start�� End ��带 ����
        if (availableNodes.Count > 0)
        {
            var shopNode = availableNodes[random.Next(availableNodes.Count)];
            shopNode.IsShop = true;
        }
        else
        {
            Debug.Log("������ ������ �� �ִ� ��尡 �����ϴ�.");
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