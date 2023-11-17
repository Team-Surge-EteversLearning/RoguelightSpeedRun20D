using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOutFits : MonoBehaviour
{
    [SerializeField]
    private List<MeshRenderer> renderers;
    [SerializeField]
    private List<Material> materials;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(int index)
    {
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = materials[index];
        }
    }
}
