using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStationChild : MonoBehaviour
{
    public bool _isFrozen;
    
    public bool IsFrozen()
    {
        return _isFrozen;
    }

    public void setFrozen(bool value)
    {
        _isFrozen = value;
    }
}
