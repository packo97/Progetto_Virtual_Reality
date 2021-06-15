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

    //private MeshRenderer _meshRenderer;

    [SerializeField] private Material deviceMaterialOn;
    [SerializeField] private Material deviceMaterialOff;

    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        //_meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int countActive = 0;
        foreach (WireCircuit wireCircuit in _wireCircuitsIN)
        {
            if (wireCircuit.active == true)
                countActive++;
        }

        if (typeOfDevice == TypeOfDevice.AND)
        {
            if (countActive == _wireCircuitsIN.Length)
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOnWire();
                active = true;
            }
            else
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOffWire();
                active = false;
            }
        }
        else if (typeOfDevice == TypeOfDevice.OR)
        {
            if (countActive >= 1)
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOnWire();
                active = true;
            }
            else
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOffWire();
                active = false;
            }
        }
        else if (typeOfDevice == TypeOfDevice.XOR)
        {
            if (countActive % 2 != 0)
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOnWire();
                active = true;
            }
            else
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOffWire();
                active = false;
            }
        }
        else if (typeOfDevice == TypeOfDevice.NXOR)
        {
            if (countActive % 2 == 0)
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOnWire();
                active = true;
            }
            else
            {
                foreach (WireCircuit wireCircuit in _wireCircuitsOUT)
                    wireCircuit.TurnOffWire();
                active = false;
            }
        }

        /* if (active)
         {
             Material[] materials = _meshRenderer.materials;
             materials[0] = deviceMaterialOn;
             _meshRenderer.materials = materials;
         }
         else
         {
             Material[] materials = _meshRenderer.materials;
             materials[0] = deviceMaterialOff;
             _meshRenderer.materials = materials;
         }*/

        if(active)
        {
            GetComponentInChildren<ParticleSystemRenderer>().material = deviceMaterialOn;
            GetComponentInChildren<Light>().color = deviceMaterialOn.GetColor("_TintColor");
        }
        else
        {
            GetComponentInChildren<ParticleSystemRenderer>().material = deviceMaterialOff;
            GetComponentInChildren<Light>().color = deviceMaterialOff.GetColor("_TintColor");
        }





    }
    
    


}
