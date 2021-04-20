using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrical : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private bool isElectricityActive;
    
    // Start is called before the first frame update
    void Start()
    {
        isElectricityActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 1 - Prendiamo il componente electrico
         * 2 - Se il l'elettricità è attiva e il generatore è associato ad una porta allora apri la porta
         * 3 - Se il l'elettricità è disattiva e il generatore è associato ad una porta allora chiudi la porta
         */
        
        ElectricBehavior electricity = GetComponentInChildren<ElectricBehavior>(true);
        isElectricityActive = electricity.gameObject.activeSelf;

        if (isElectricityActive && door != null)
        {
            door.GetComponent<DoorInput>().OpenDoor();
        }
        else if (!isElectricityActive && door != null)
        {
            door.GetComponent<DoorInput>().CloseDoor();
        }
    }
}
