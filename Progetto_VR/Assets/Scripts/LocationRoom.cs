using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationRoom : MonoBehaviour
{
    [System.Serializable] public enum Room { Room_3 = 3, Room_4, Room_5, Room_6, Room_7 };

    public Room room;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        /*
         *  Imposto la stanza in cui si trova il Player
         * 
         */
        
        if (other.tag=="Player")
        {
            other.GetComponent<PlayerController>().SetRoom((int)room);
            
        }
    }

}
