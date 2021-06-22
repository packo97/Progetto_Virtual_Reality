using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressureForElectric : MonoBehaviour
{
    [SerializeField] 
    private GameObject electric_cannon1;
    
    [SerializeField] 
    private GameObject electric_cannon2;
    [SerializeField] 
    private GameObject electric_cannon3;
    
    [SerializeField] 
    private GameObject electric_cannon4;
    
    private void OnTriggerEnter(Collider other)
    {
        /*
         * Se sono su una pedana -> effettua lo switch dei corrispondenti Electric Cannon Gate
         * 
         */
        
        
        if(electric_cannon1)
            electric_cannon1.GetComponent<ElectricBehavior>().ElectricSwitch();
        if(electric_cannon2)
            electric_cannon2.GetComponent<ElectricBehavior>().ElectricSwitch();
        if(electric_cannon3)
            electric_cannon3.GetComponent<ElectricBehavior>().ElectricSwitch();
        if(electric_cannon4)
            electric_cannon4.GetComponent<ElectricBehavior>().ElectricSwitch();
        
        GetComponent<AudioSource>().Play();
    }
}
