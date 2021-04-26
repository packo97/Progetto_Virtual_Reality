using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    
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
        Transfomation transfomation = other.gameObject.GetComponent<Transfomation>();
        
        if (other.gameObject.tag.Equals("Player"))
        {
            if (transfomation.transf == Transfomation.Transformation.Ghiaccio && gameObject.tag.Equals("Water"))
                ;
            else
            {
                other.gameObject.transform.position = playerController.getRespawnPosition();
                playerController.Hurt();
            }
            
           
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        /*
         * 1 - Se collide con particelle che sono taggate cond "Die" -> cambia la posizione attuale nella respawn position
         *     e diminuisci la vita di 1.
         */
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (other.tag.Equals("Player"))
        {
            other.gameObject.transform.position = playerController.getRespawnPosition();
            playerController.Hurt();
        }
    }
}
