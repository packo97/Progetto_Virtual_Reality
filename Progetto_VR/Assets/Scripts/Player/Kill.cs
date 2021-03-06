using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public enum TypeOfKill {Electricity, Water, Acid, Fall};

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        /*
         *  1 - Se collide con un oggetto che è taggato con "Die" -> Imposta la posizione attuale alla respawn position
         *     e diminuisci la vita di 1.
         */

        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        Transformation transfomation = other.gameObject.GetComponent<Transformation>();
        
        if (other.gameObject.tag.Equals("Player"))
        {
            if (transfomation.transf == Transformation.TypeOfTransformation.Ghiaccio && gameObject.tag.Equals("Water"))
                ;
            else
            {
                other.gameObject.transform.position = playerController.getRespawnPosition();
                playerController.Hurt(TypeOfKill.Water);
            }
            
            
            
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*
         * Se entro nell'area di trigger, muoio sotto alcune condizioni.
         * 
         */
        
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
            if (other.gameObject.tag.Equals("Player") && (other.gameObject.GetComponent<Transformation>().transf != Transformation.TypeOfTransformation.Gomma && other.gameObject.GetComponent<Transformation>().transf != Transformation.TypeOfTransformation.Rame))
            {
                playerController.Hurt(TypeOfKill.Electricity);
            }
    }
}
