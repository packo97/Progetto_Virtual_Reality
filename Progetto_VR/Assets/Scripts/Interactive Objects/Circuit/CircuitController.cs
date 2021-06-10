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
