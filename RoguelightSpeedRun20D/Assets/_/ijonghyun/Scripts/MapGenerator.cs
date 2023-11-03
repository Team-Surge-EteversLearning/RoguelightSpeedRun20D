using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<RoomData> roomDatas = new List<RoomData>(); 
    public int gridSizeX = 5; 
    public int gridSizeZ = 5; 
    public float gridSpacing = 10f; 
    public float placementProbability = 0.3f; 
    public int minimumRoomCount = 5; 
    private bool[,] visited; 
    private int roomCount; 

    void Start()
    {
        visited = new bool[gridSizeX, gridSizeZ];
        roomCount = 0;
        GenerateMap();
    }

    void GenerateMap()
    {
        
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeZ; j++)
            {
                visited[i, j] = false;
            }
        }

        
        int startX = Random.Range(0, gridSizeX);
        int startZ = Random.Range(0, gridSizeZ);
        RoomData startRoomData = roomDatas[Random.Range(0, roomDatas.Count)];
        Instantiate(startRoomData.roomPrefab, new Vector3(startX * gridSpacing, 0f, startZ * gridSpacing), Quaternion.identity);
        visited[startX, startZ] = true;
        roomCount++;

        
        List<Vector2Int> connectedGrids = new List<Vector2Int>();
        DFS(startX, startZ, connectedGrids);

        
        while (roomCount < minimumRoomCount)
        {
            int randomX = Random.Range(0, gridSizeX);
            int randomZ = Random.Range(0, gridSizeZ);

            if (!visited[randomX, randomZ])
            {
                RoomData randomRoomData = roomDatas[Random.Range(0, roomDatas.Count)];
                Instantiate(randomRoomData.roomPrefab, new Vector3(randomX * gridSpacing, 0f, randomZ * gridSpacing), Quaternion.identity);
                visited[randomX, randomZ] = true;
                roomCount++;
            }
        }
    }

    
    void DFS(int x, int z, List<Vector2Int> connectedGrids)
    {
        if (x < 0 || x >= gridSizeX || z < 0 || z >= gridSizeZ || visited[x, z] || Random.Range(0f, 1f) > placementProbability)
        {
            return;
        }

        visited[x, z] = true;
        connectedGrids.Add(new Vector2Int(x, z));
        roomCount++;

        
        DFS(x + 1, z, connectedGrids);
        DFS(x - 1, z, connectedGrids);
        DFS(x, z + 1, connectedGrids);
        DFS(x, z - 1, connectedGrids);
    }
}

   
