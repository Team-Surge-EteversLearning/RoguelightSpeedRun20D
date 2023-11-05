using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOutfitHandle : MonoBehaviour
{
    [SerializeField]
    private List<Material> materials;
    [SerializeField]
    private MeshRenderer meshRenderer;

    public int indexMaterial { set { meshRenderer.material = materials[value]; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
