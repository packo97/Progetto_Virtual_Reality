using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitController : MonoBehaviour
{
    [SerializeField] private DeviceCircuit[] _deviceCircuits;
    [SerializeField] private DoorInput door;
    [SerializeField] private DoorInput door2;
    [SerializeField] private DeviceCircuit _specialDevice;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Verifico il numero di device attivi.
         * Se sono tutti attivi tranne lo special -> apro la porta 1
         * Se sono tutti attivi -> apro la porta 2
         * 
         */
        
        int countActive = 0;
        foreach (DeviceCircuit deviceCircuit in _deviceCircuits)
        {
            if (deviceCircuit.active == true)
                countActive++;
        }

        if (countActive == _deviceCircuits.Length - 1 && !_specialDevice.active)
        {
            door.OpenDoor();
        }
        else
        {
            door.CloseDoor();
        }

        if (countActive == _deviceCircuits.Length)
        {
            door2.OpenDoor();
        }
        else
        {
            door2.CloseDoor();
        }
    }
}
