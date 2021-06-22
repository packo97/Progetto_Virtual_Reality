using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerIceStation : MonoBehaviour
{
    [SerializeField] private IceStation[] _iceStations;

    [SerializeField] private DoorInput _door;

    // Update is called once per frame
    void Update()
    {
        /*
         *  Apri la porta se tutte le stazioni sono congelate
         */
        
        if (allFrozen())
            _door.OpenDoor();
            
    }

    private bool allFrozen()
    {
        /*
         * Se tutte le stazione sono congelate, restituisco True
         * 
         */
        foreach (IceStation iceStation in _iceStations)
        {
            if (!iceStation.IsFrozenStation())
                return false;
        }

        return true;
    }
}
