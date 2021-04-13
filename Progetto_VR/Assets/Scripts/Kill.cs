using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    /*[SerializeField] private GameObject player;*/

   /* [SerializeField] 
    private Transform respawn;*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            /*player.SetActive(false);
            player.transform.position = respawn.transform.position;
            player.SetActive(true);*/
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals("Player"))
        {
            /* player.SetActive(false);
             player.transform.position = respawn.transform.position;
             player.SetActive(true);*/
            other.SetActive(false);
            other.transform.position = other.GetComponent<PlayerRespawn>().GetRespawn();
            other.SetActive(true);
        }
        
    }

    
}
