using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    [SerializeField] GameObject miniNodePrefab;
    [SerializeField] GameObject miniMapSet;
    private Dictionary<DungeonNode, GameObject> miniMapDict = new Dictionary<DungeonNode, GameObject>();

    private void Awake()
    {
        GameObject miniSet = Instantiate(miniMapSet);
    }
    public void MiniMapCreate(Dungeon dungeon)
    {
        foreach(var node in dungeon)
        {
            GameObject miniNode = Instantiate(miniNodePrefab);
            miniNode.name = "mini" + node.Position.ToString();
            miniNode.transform.position = new Vector3(node.Position.x, node.Position.y, node.Position.z);
            miniMapDict.Add(node, miniNode);
            if (node.Position.y != 0)
            {
                miniNode.SetActive(false);
            }
        }
    }
    public void ChangeMinimapNode(DungeonNode current, DungeonNode prv = null)
    {
        if(current.Position.y != prv.Position.y)
        {
            ChangeFloorForMiniMap(current.Position.y);
        }
        if (miniMapDict[current].GetComponentsInChildren<SpriteRenderer>()[1].enabled)
            miniMapDict[current].GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        if (prv != null)
            miniMapDict[prv].GetComponent<SpriteRenderer>().color = Color.white;
        if (miniMapDict.ContainsKey(current))
            miniMapDict[current].GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void ChangeFloorForMiniMap(int next)
    {
        foreach (var item in miniMapDict)
        {
            if (item.Key.Position.y == next)
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
