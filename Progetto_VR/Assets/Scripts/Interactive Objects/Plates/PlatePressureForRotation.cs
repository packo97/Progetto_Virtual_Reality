using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressureForRotation : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    private void OnTriggerEnter(Collider other)
    {
       /*
        * Quando la pedana è premuta, faccio partire la rotazione delle piattaforme rotanti
        */

        foreach (GameObject obj in gameObjects)
        {
            if (obj.GetComponent<Rotation>() && other.gameObject.GetComponent<PlayerController>())
                StartCoroutine(obj.GetComponent<Rotation>().ObjectRotation());
        }    
        
        GetComponent<AudioSource>().Play();    
    }
    
}
