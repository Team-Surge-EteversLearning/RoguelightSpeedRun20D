using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DungeonNode
{
    public DungeonNode Left { get; set; }
    public DungeonNode Right { get; set; }
    public DungeonNode Front { get; set; }
    public DungeonNode Back { get; set; }
    public bool IsShop { get; set; }
    public int DistanceFromStart { get; set; }
    public Vector2Int Position { get; set; }

    public DungeonNode(int distance)
    {
        DistanceFromStart = distance;
    }
}

public class Dungeon : IEnumerable<DungeonNode>
{
    public DungeonNode Start { get; private set; }
    public DungeonNode End { get; private set; }
    private List<DungeonNode> nodes = new List<DungeonNode>();

    public Dungeon()
    {
        Start = new DungeonNode(0);
        nodes.Add(Start);
    }


    public void Add()
    {
        var node = new DungeonNode(0);  // Initialize with 0 DistanceFromStart
        nodes.Add(node);

        if (nodes.Count == 1)
        {
            node.Position = Vector2Int.zero;  // 첫 노드의 위치를 (0,0)으로 초기화
            node.DistanceFromStart = 0;
        }
        else
        {
            var random = new Random();
            var availableNodes = nodes.Where(n => n.Left == null || n.Right == null || n.Front == null || n.Back == null).ToList();
            DungeonNode selectedNode = null;

            // Keep trying until we find a node with an available direction
            while (availableNodes.Count > 0)
            {
                selectedNode = availableNodes[random.Next(availableNodes.Count)];

                // Define all possible directions
                var directions = new List<Tuple<Action, Func<Vector2Int>>>
            {
                new Tuple<Action, Func<Vector2Int>>(() => selectedNode.Left = node, () => selectedNode.Position + Vector2Int.left),
                new Tuple<Action, Func<Vector2Int>>(() => selectedNode.Right = node, () => selectedNode.Position + Vector2Int.right),
                new Tuple<Action, Func<Vector2Int>>(() => selectedNode.Front = node, () => selectedNode.Position + Vector2Int.up),
                new Tuple<Action, Func<Vector2Int>>(() => selectedNode.Back = node, () => selectedNode.Position + Vector2Int.down)
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
                        var surroundingPositions = new List<Tuple<Action, Vector2Int>>
                    {
                        new Tuple<Action, Vector2Int>(() => node.Left = nodes.First(n => n.Position == newPosition + Vector2Int.left), newPosition + Vector2Int.left),
                        new Tuple<Action, Vector2Int>(() => node.Right = nodes.First(n => n.Position == newPosition + Vector2Int.right), newPosition + Vector2Int.right),
                        new Tuple<Action, Vector2Int>(() => node.Front = nodes.First(n => n.Position == newPosition + Vector2Int.up), newPosition + Vector2Int.up),
                        new Tuple<Action, Vector2Int>(() => node.Back = nodes.First(n => n.Position == newPosition + Vector2Int.down), newPosition + Vector2Int.down)
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

                if (node.Position != Vector2Int.zero)
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
        End = nodes.OrderByDescending(n => n.DistanceFromStart).First();
    }
    public void AddUntil(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            this.Add();
        }
    }


    public void SetShopNode()
    {
        var random = new Random();
        var availableNodes = nodes.Where(n => n != Start && n != End).ToList();  // Start와 End 노드를 제외
        if (availableNodes.Count > 0)
        {
            var shopNode = availableNodes[random.Next(availableNodes.Count)];
            shopNode.IsShop = true;
        }
        else
        {
            Debug.Log("상점을 설정할 수 있는 노드가 없습니다.");
        }
    }

    public static void ConnectFloor(Dungeon startFloor, Dungeon arriveFloor)
    {
        if (startFloor == arriveFloor)
        {
            Debug.Log("You try Connect same floor");
            return;
        }

        // Connect startFloor's end to arriveFloor's start
        if (startFloor.End.Front == null)
        {
            startFloor.End.Front = arriveFloor.Start;
        }
        else if (startFloor.End.Back == null)
        {
            startFloor.End.Back = arriveFloor.Start;
        }
        else if (startFloor.End.Left == null)
        {
            startFloor.End.Left = arriveFloor.Start;
        }
        else if (startFloor.End.Right == null)
        {
            startFloor.End.Right = arriveFloor.Start;
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
