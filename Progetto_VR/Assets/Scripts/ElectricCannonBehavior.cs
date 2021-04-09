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
        //Debug.Log("asasasa");
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();

// Debug.Log(collisionEvents);
        int i = 0;

        
            if (rb)
            {
                /*
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;               
                rb.AddForce(force);
                */
                other.GetComponent<PlayerBehavior>().Hurt();
            }
            i++;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
