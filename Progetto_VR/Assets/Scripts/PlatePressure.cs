using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressure : MonoBehaviour
{
    private bool pressure = false;
    private MeshRenderer _render;

    [SerializeField]
    private GameObject door;
    
    [SerializeField]
    private Material _triggerOn;

    [SerializeField]
    private Material _triggerOff;

    // Start is called before the first frame update
    void Start()
    {
        _render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    //cambiamo il colore alla piattaforma da rosso a verde e chiama lo scritp
    private void OnTriggerStay(Collider other)
    {
        if (pressure == false && other.GetType() != typeof(SphereCollider))
        {
            pressure = true;
            door.GetComponent<DoorInput>().OpenDoor();

            Material[] materials = _render.materials;
            materials[2] = _triggerOn;
            _render.materials = materials;
        }
    }

    //cambiamo il colore alla piattaforma da rosso a verde e chiama lo scritp
    private void OnTriggerExit(Collider other)
    {
        if (pressure == true && other.GetType() != typeof(SphereCollider))
        {
            pressure = false;
            door.GetComponent<DoorInput>().CloseDoor();

            Material[] materials = _render.materials;
            materials[2] = _triggerOff;
            _render.materials = materials;
        }
    }
}
