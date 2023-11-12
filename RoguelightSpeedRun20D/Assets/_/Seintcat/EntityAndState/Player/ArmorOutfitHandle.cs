using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorOutfitHandle : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> armorParts;
    [SerializeField]
    private List<List<Material>> materials;

    public int materialIndex
    {
        set
        {
            for(int i = 0; i < armorParts.Count; i++)
                armorParts[i].material = materials[i][value];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
