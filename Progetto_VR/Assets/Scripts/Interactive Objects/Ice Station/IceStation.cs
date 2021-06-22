using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class IceStation : MonoBehaviour
{
    [SerializeField] private ElectricBehavior particleCannon;
    private IceStationChild[] sideChild;
    private bool frozenStation;

    // Start is called before the first frame update
    void Start()
    {
        sideChild = GetComponentsInChildren<IceStationChild>();
    }

    // Update is called once per frame
    void Update()
    {
        IsStationActive();
    }

    private bool IsStationActive()
    {
        /*
         * Controllo se la stazione deve essere attivata.
         * Se va attivata, attivo il raggio elettrico
         * 
         */
        
        if (frozenStation)
            return true;
        
        foreach (IceStationChild child in sideChild)
        {
            if (child.IsFrozen() == false)
            {
                return false;
            }
        }

        frozenStation = true;
        particleCannon.gameObject.SetActive(true);
        return true;
    }

    public bool IsFrozenStation()
    {
        return frozenStation;
    }
    
}
