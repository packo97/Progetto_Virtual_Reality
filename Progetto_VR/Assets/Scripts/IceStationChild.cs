using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStationChild : MonoBehaviour
{
    public bool _isFrozen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFrozen()
    {
        return _isFrozen;
    }

    public void setFrozen(bool value)
    {
        _isFrozen = value;
    }
}
