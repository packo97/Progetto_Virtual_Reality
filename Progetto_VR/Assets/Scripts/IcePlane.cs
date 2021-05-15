using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlane : MonoBehaviour
{
    private IceStationChild iceStationChild;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator IceTime()
    {
        /*
         * Dopo 8 secondi se la stazione di riferimento non è attiva, distrutto il plane di ghiaccio 
         */
        
        yield return new WaitForSeconds(8);

        IceStation station = null;
        if (iceStationChild != null)
        {
            station = iceStationChild.GetComponentInParent<IceStation>();
            if(!station.IsFrozenStation())
                Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        /*
         * Se collido con un lato della stazione di ghiaccio, lo setto a frozen e mi salvo il riferimento
         * 
         */
        
        if (other.gameObject.GetComponent<IceStationChild>() != null)
        {
            iceStationChild = other.gameObject.GetComponent<IceStationChild>();
            iceStationChild.setFrozen(true);
        }
    }

    private void OnDestroy()
    {
        /*
         * Scongela il lato che era ghiacciato se presente
         */
        if (iceStationChild != null)
            iceStationChild.setFrozen(false);
    }
}
