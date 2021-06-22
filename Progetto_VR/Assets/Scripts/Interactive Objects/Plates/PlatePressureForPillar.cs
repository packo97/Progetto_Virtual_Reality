using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressureForPillar : MonoBehaviour
{
    [SerializeField] private PillarMovement[] pillars;
    
    private void OnTriggerEnter(Collider other)
    {
        /*
         * Se sono su una pedana -> effettua lo switch dei corrispondenti Electric Cannon Gate
         * 
         */

        bool someoneIsMoving = false;
        foreach (PillarMovement pillarMovement in pillars)
        {
            if (pillarMovement.IsMoving())
                someoneIsMoving = true;
        }
        
        if (!someoneIsMoving)
            foreach (PillarMovement pillarMovement in pillars)
            {
                pillarMovement.Movement();
            }
        
        GetComponent<AudioSource>().Play();
    }
}
