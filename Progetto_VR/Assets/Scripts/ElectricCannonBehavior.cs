using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCannonBehavior : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
 
    }

    void OnParticleCollision(GameObject other)
    {
        PlayerBehavior player = other.GetComponentInParent<PlayerBehavior>();

        if (player)
        {
            player.Hurt();
        }

        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
