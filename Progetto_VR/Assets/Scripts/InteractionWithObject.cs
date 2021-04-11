using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObject : MonoBehaviour
{
    private GameObject objectInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (objectInRange != null)
            {
                Debug.Log("Preso");
                objectInRange.SetActive(false);
            }
            else
            {
                Debug.Log("Droppa");
                objectInRange.transform.position = gameObject.transform.position;
                objectInRange.SetActive(true);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.GetComponent<Interactable>())
        {
            Debug.Log("L'oggetto " + other.gameObject.name + "è interagibile");
            objectInRange = obj;
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        objectInRange = null;
    }
    */
}
