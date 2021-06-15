using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCircuit : MonoBehaviour
{
    public bool active;
    private MeshRenderer[] _meshRenderers;

    [SerializeField] private Material wireMaterialOn;
    [SerializeField] private Material wireMaterialOff;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnWire()
    {
        active = true;

        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            Material[] materials = meshRenderer.materials;
            materials[0] = wireMaterialOn;
            meshRenderer.materials = materials;   
        }
            

        
    }

    public void TurnOffWire()
    {
        active = false;

        
        foreach (MeshRenderer meshRenderer in _meshRenderers)
        {
            Material[] materials = meshRenderer.materials;
            materials[0] = wireMaterialOff;
            meshRenderer.materials = materials;
        }
    }
}
