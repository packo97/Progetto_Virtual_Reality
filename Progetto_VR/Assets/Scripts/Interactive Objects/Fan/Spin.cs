using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 3.0f;

    void Update()
    {
        /*
         * Rotazione dell'oggetto
         * 
         */
        
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }

    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }

    private void OnDisable()
    {
        GetComponent<AudioSource>().Stop();
    }
}
