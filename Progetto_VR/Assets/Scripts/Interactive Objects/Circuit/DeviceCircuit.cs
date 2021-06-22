using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class DeviceCircuit : MonoBehaviour
{
    public enum TypeOfDevice {AND = 0, OR, XOR, NXOR};
    public TypeOfDevice typeOfDevice;
    [SerializeField] private WireCircuit[] _wireCircuitsIN;
    [SerializeField] private WireCircuit[] _wireCircuitsOUT;

   

    [SerializeField] private Material deviceMaterialOn;
    [SerializeField] private Material deviceMaterialOff;

    public bool active;

    private int countActive = 0;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
      
    }

    public void AddActivePipe()
    {
        /*
         * Attivo tubo e aggiorno il contatore
         */
        
        countActive++;
        TryActiveDevice();
    }

    public void RemoveActivePipe()
    {
        /*
         * Disattivo tubo e aggiorno il contatore
         */
        countActive--;
        TryActiveDevice();
    }

    public void TryActiveDevice()
    {
        /*
         * Verifica a seconda della condizione se attivare o meno il Device
         * 
         */
        
        if (typeOfDevice == TypeOfDevice.AND)
        {
            if (countActive == _wireCircuitsIN.Length)
            {
                active = true;
                TurnOnDevice();
            }
            else
            {
                active = false;
                TurnOffDevice();
            }
        }
        else if (typeOfDevice == TypeOfDevice.OR)
        {
            if (countActive >= 1)
            {
                active = true;
                TurnOnDevice();
            }
            else
            {
                active = false;
                TurnOffDevice();
            }
        }
        else if (typeOfDevice == TypeOfDevice.XOR)
        {
            if (countActive % 2 != 0)
            {
                active = true;
                TurnOnDevice();
            }
            else
            {
                active = false;
                TurnOffDevice();
            }
        }
        else if (typeOfDevice == TypeOfDevice.NXOR)
        {
            if (countActive % 2 == 0)
            {
                active = true;
                TurnOnDevice();
            }
            else
            {
                active = false;
                TurnOffDevice();
            }
        }
    }

    private void TurnOnDevice()
    {
        /*
         * Attiva il device e il relativo tubo di output
         * 
         */
        
        GetComponentInChildren<ParticleSystemRenderer>().material = deviceMaterialOn;
        GetComponentInChildren<Light>().color = deviceMaterialOn.GetColor("_TintColor");

        foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
            wireCircuit.TurnOnWire();
    }

    public void TurnOffDevice()
    {
        /*
         * Disattiva il device e il relativo tubo di output
         * 
         */
        
        GetComponentInChildren<ParticleSystemRenderer>().material = deviceMaterialOff;
        GetComponentInChildren<Light>().color = deviceMaterialOff.GetColor("_TintColor");

        foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
            wireCircuit.TurnOffWire();
    }

    

}
