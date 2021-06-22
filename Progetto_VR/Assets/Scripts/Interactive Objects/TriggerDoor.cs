using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] private DoorInput door;

    private void OnTriggerEnter(Collider other)
    {
        /*
         * Apro la porta quando si entra nell'area di trigger
         * 
         */
        
        if (other.GetComponent<PlayerController>())
        {
            door.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*
         * Chiudo la porta quando si esce dall'area di trigger
         * 
         */
        
        if (other.GetComponent<PlayerController>())
        {
            door.CloseDoor();
        }
    }
}
