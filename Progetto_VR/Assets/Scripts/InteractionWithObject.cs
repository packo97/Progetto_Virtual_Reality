using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObject : MonoBehaviour
{
    private GameObject objectInRange;
    private bool isTaken = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Con la pressione del tasto "E" è possibile prendere gli oggetti che possiedono lo script "Interactable" e sono nel range del collider
         * Se si possiede un oggetto, premendo il tasto "E" si rilascia l'oggetto nella posizione in cui si trova il player
         * 
         */
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectInRange != null && !isTaken)
            {
                objectInRange.SetActive(false);
                isTaken = true;
            }
            else if (isTaken)
            {
                objectInRange.transform.position = gameObject.transform.position;
                objectInRange.SetActive(true);
                isTaken = false;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*
         * Se sei nel range di un oggetto che possiede lo script "Interactable", tienilo in memoria
         * 
         */
        
        GameObject obj = other.gameObject;

        if (obj.GetComponent<Interactable>())
        {
            objectInRange = obj;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        /*
         * Se esci dal range di un oggetto che possiede lo script "Interactable", non tenerlo più in memoria
         */
        GameObject obj = other.gameObject;

        if (obj.GetComponent<Interactable>())
        {
            objectInRange = null;
        }
    }
    
}
