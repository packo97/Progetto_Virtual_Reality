using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePressureForPillar : MonoBehaviour
{
    [SerializeField]
    private GameObject pillar1;

    [SerializeField]
    private GameObject pillar2;
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
        /*
         * Se sono su una pedana -> effettua lo switch dei corrispondenti Electric Cannon Gate
         * 
         */


        if (pillar1)
            pillar1.GetComponent<PillarMovement>().Movement();
        if (pillar2)
            pillar2.GetComponent<PillarMovement>().Movement();

    }
}
