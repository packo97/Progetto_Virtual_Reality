using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCannonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        PlayerBehavior player = other.GetComponent<PlayerBehavior>();

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
