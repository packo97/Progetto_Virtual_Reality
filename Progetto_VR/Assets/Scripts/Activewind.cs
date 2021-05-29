using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activewind : MonoBehaviour
{

    [SerializeField] GameObject[] ToEnable;
    [SerializeField] GameObject[] ToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (ToEnable != null)
            for (int i = 0; i < ToEnable.Length; i++)
                ToEnable[i].GetComponent<WindForce>().enabled= true;
        if (ToDisable != null)
            for (int i = 0; i < ToDisable.Length; i++)
                ToDisable[i].GetComponent<WindForce>().enabled=false;
    }
}
