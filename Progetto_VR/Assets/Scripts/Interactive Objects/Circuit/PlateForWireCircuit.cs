using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForWireCircuit : MonoBehaviour
{
    [SerializeField] private WireCircuit _wireCircuit;
    
    private void OnTriggerEnter(Collider other)
    {
        /*
         * Attiva tubo quando la plate viene triggerata
         * 
         */
        
        if (!_wireCircuit.active)
            _wireCircuit.TurnOnWire();
        else
        {
            _wireCircuit.TurnOffWire();
        }
        
        GetComponent<AudioSource>().Play();
    }


}
