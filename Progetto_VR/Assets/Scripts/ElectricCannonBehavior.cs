using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ElectricCannonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
 
    }

    void OnParticleCollision(GameObject other)
    {
        /*PlayerBehavior player = other.GetComponentInParent<PlayerBehavior>();

        if (player)
        {
            player.Hurt();
        }
        */
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ElectricSwitch()
    {
   
            Transform[] list = GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < list.Length; i++)
            {
                
                if (list[i].GetComponent<ParticleSystem>())
                {
                    bool value = list[i].gameObject.activeSelf;
                    //domanda per il prof
                    if (value == true)
                    {
                        list[i].gameObject.SetActive(false);
                        
                    }
                    else
                    {
                        list[i].gameObject.SetActive(true);
                    }
                }
                Debug.Log(list[i].name + list[i].gameObject.activeSelf);
            }

    }
    
    
}
