using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = other.gameObject.GetComponent<PlayerRespawn>().GetRespawn();
            other.gameObject.SetActive(true);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals("Player"))
        {
            other.SetActive(false);
            other.transform.position = other.GetComponent<PlayerRespawn>().GetRespawn();
            other.SetActive(true);
        }
        
    }

    
}
