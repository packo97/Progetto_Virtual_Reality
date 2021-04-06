using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressure : MonoBehaviour
{
    private BoxCollider _collider;
    private bool pressure = false;
    private Renderer _render;
    private GameObject door;
    [SerializeField]
    private Material _triggerOn;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _render = GetComponent<Renderer>();
        door = GameObject.Find("door");
    }

    // Update is called once per frame
    void Update()
    {
        _render.materials[2] = _triggerOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pressure == false)
        {
            pressure = true;
            door.GetComponent<DoorInput>().OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (pressure == true)
        {
            pressure = false;
            door.GetComponent<DoorInput>().CloseDoor();
        }
    }
}
