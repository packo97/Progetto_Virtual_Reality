using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPressable : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private BoxCollider collider;
    [SerializeField] private Material pressedMaterialPlayer1;
    [SerializeField] private Material pressedMaterialPlayer2;
    [SerializeField] private Material pressedMaterialDefault;

    public bool active;

    private bool isInvisible;

    PlatformPressableController controller;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        active = false;
        isInvisible = false;
        collider = GetComponent<BoxCollider>();
        controller = GetComponentInParent<PlatformPressableController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        Material[] materials = _meshRenderer.materials;
        if (!isInvisible)
        {
            if (!active)
            {
                if (other.GetComponent<PlayerController>()){
                    materials[0] = pressedMaterialPlayer1;
                    GetComponent<AudioSource>().Play();
                }
                else if (other.GetComponent<FriendPlayer>()){
                    GetComponent<AudioSource>().Play();
                    materials[0] = pressedMaterialPlayer2;
                }
                active = true;
            }
            else
            {
                materials[0] = pressedMaterialDefault;
                active = false;
                StartCoroutine(InvisibleTime());
            }
        
            _meshRenderer.materials = materials;    
        }
        
    }

    private IEnumerator InvisibleTime()
    {
        isInvisible = true;
        collider.isTrigger = true;
        //_meshRenderer.enabled = false;
        controller.ResetAll();
        yield return new WaitForSeconds(3);
        collider.isTrigger = false;
        //_meshRenderer.enabled = true;
        isInvisible = false;

    }

    public void Reset()
    {
        Material[] materials = _meshRenderer.materials;  
        materials[0] = pressedMaterialDefault;
        _meshRenderer.materials = materials;
        active = false;
    }
}
