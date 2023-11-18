using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOutFits : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> renderers;
    [SerializeField]
    private List<Material> materials;

    public int materialIndex
    {
        set
        {
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material = materials[value];
            }
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
