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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ElectricSwitch()
    {
        /*
         * 1 - Prendo tutti i figli dell'oggetto electric cannon gate
         * 2 - Controllo se possiedono il componente Particle System
         * 3 - Gli oggetti che lo hanno se sono attivi vengono disattivati e viceversa
         * 
         */
        
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
        }
    }
    
    
}
