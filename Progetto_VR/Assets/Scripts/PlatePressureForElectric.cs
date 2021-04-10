using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressureForElectric : MonoBehaviour
{
    [SerializeField] private GameObject electric_cannon1;
    [SerializeField] private GameObject electric_cannon2;
    [SerializeField] private GameObject electric_cannon3;
    [SerializeField] private GameObject electric_cannon4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(electric_cannon1)
            electric_cannon1.GetComponent<ElectricCannonBehavior>().ElectricSwitch();
        if(electric_cannon2)
            electric_cannon2.GetComponent<ElectricCannonBehavior>().ElectricSwitch();
        if(electric_cannon3)
            electric_cannon3.GetComponent<ElectricCannonBehavior>().ElectricSwitch();
        if(electric_cannon4)
            electric_cannon4.GetComponent<ElectricCannonBehavior>().ElectricSwitch();
    }
}
