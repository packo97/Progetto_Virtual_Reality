using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressureForRotation : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on trigger enter con " + other.name);

        foreach (GameObject obj in gameObjects)
        {
            if (obj.GetComponent<Rotation>() && other.gameObject.GetComponent<PlayerController>())
                StartCoroutine(obj.GetComponent<Rotation>().ObjectRotation());
        }    
        
            
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
    }
}
