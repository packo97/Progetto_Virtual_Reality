using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForWireCircuit : MonoBehaviour
{
    [SerializeField] private WireCircuit _wireCircuit;
    
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
        if (!_wireCircuit.active)
            _wireCircuit.TurnOnWire();
        else
        {
            _wireCircuit.TurnOffWire();
        }
        
        GetComponent<AudioSource>().Play();
    }


}
