using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    [SerializeField] GameObject miniNodePrefab;
    [SerializeField] GameObject miniMapCamera;
    [SerializeField] Sprite bossMark;
    private Dictionary<DungeonNode, GameObject> miniMapDict = new Dictionary<DungeonNode, GameObject>();

    private void Awake()
    {
    }
    public void MiniMapCreate()
    {
        foreach(var node in DungeonManager.Instance.Dungeon)
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
        if (current.Position.y != prv.Position.y && prv != null)
        {
            Debug.LogWarning(current.Position.y);
            miniMapCamera.transform.position = new Vector3(DungeonManager.Instance.Dungeon.Starts[current.Position.y].Position.x, 100, DungeonManager.Instance.Dungeon.Starts[current.Position.y].Position.z);
            ChangeFloorForMiniMap(current.Position.y);
        }
        if (miniMapDict[current].GetComponentsInChildren<SpriteRenderer>()[1].enabled)
        {
            if (DungeonManager.Instance.Dungeon.Ends.Contains(current))
            {
                miniMapDict[current].GetComponentsInChildren<SpriteRenderer>()[1].sprite = bossMark;
            }
            else
                miniMapDict[current].GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        }
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
